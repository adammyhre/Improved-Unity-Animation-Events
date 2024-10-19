using UnityEngine;
using System.Collections.Generic;

public class AnimationEventReceiver : MonoBehaviour {
    [SerializeField] List<AnimationEvent> animationEvents = new();

    public void OnAnimationEventTriggered(string eventName) {
        AnimationEvent matchingEvent = animationEvents.Find(se => se.eventName == eventName);
        matchingEvent?.OnAnimationEvent?.Invoke();
    }
}