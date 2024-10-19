using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomEditor(typeof(AnimationEventStateBehaviour))]
public class AnimationEventStateBehaviourEditor : Editor {
    AnimationClip previewClip;
    float previewTime;
    bool isPreviewing;

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        AnimationEventStateBehaviour stateBehaviour = (AnimationEventStateBehaviour) target;

        if (Validate(stateBehaviour, out string errorMessage)) {
            GUILayout.Space(10);

            if (isPreviewing) {
                if (GUILayout.Button("Stop Preview")) {
                    EnforceTPose();
                    isPreviewing = false;
                } else {
                    PreviewAnimationClip(stateBehaviour);
                }
            } else if (GUILayout.Button("Preview")) {
                isPreviewing = true;
            }

            GUILayout.Label($"Previewing at {previewTime:F2}s", EditorStyles.helpBox);
        } else {
            EditorGUILayout.HelpBox(errorMessage, MessageType.Info);
        }
    }

    void PreviewAnimationClip(AnimationEventStateBehaviour stateBehaviour) {
        if (previewClip == null) return;

        previewTime = stateBehaviour.triggerTime * previewClip.length;

        AnimationMode.StartAnimationMode();
        AnimationMode.SampleAnimationClip(Selection.activeGameObject, previewClip, previewTime);
        AnimationMode.StopAnimationMode();
    }

    bool Validate(AnimationEventStateBehaviour stateBehaviour, out string errorMessage) {
        AnimatorController animatorController = GetValidAnimatorController(out errorMessage);
        if (animatorController == null) return false;

        ChildAnimatorState matchingState = animatorController.layers
            .SelectMany(layer => layer.stateMachine.states)
            .FirstOrDefault(state => state.state.behaviours.Contains(stateBehaviour));

        previewClip = matchingState.state?.motion as AnimationClip;
        if (previewClip == null) {
            errorMessage = "No valid AnimationClip found for the current state.";
            return false;
        }

        return true;
    }

    AnimatorController GetValidAnimatorController(out string errorMessage) {
        errorMessage = string.Empty;

        GameObject targetGameObject = Selection.activeGameObject;
        if (targetGameObject == null) {
            errorMessage = "Please select a GameObject with an Animator to preview.";
            return null;
        }

        Animator animator = targetGameObject.GetComponent<Animator>();
        if (animator == null) {
            errorMessage = "The selected GameObject does not have an Animator component.";
            return null;
        }

        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
        if (animatorController == null) {
            errorMessage = "The selected Animator does not have a valid AnimatorController.";
            return null;
        }

        return animatorController;
    }

    [MenuItem("GameObject/Enforce T-Pose", false, 0)]
    static void EnforceTPose() {
        GameObject selected = Selection.activeGameObject;
        if (!selected || !selected.TryGetComponent(out Animator animator) || !animator.avatar) return;

        SkeletonBone[] skeletonBones = animator.avatar.humanDescription.skeleton;

        foreach (HumanBodyBones hbb in Enum.GetValues(typeof(HumanBodyBones))) {
            if (hbb == HumanBodyBones.LastBone) continue;

            Transform boneTransform = animator.GetBoneTransform(hbb);
            if (!boneTransform) continue;

            SkeletonBone skeletonBone = skeletonBones.FirstOrDefault(sb => sb.name == boneTransform.name);
            if (skeletonBone.name == null) continue;

            if (hbb == HumanBodyBones.Hips) boneTransform.localPosition = skeletonBone.position;
            boneTransform.localRotation = skeletonBone.rotation;
        }
    }
}