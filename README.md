# Improved Unity Animation Events

![AnimationEvents](https://github.com/user-attachments/assets/ab3b9e80-1533-454b-b551-78ff8d92169f)

This repository offers a powerful, flexible system for managing **Animation Events** in Unity, with advanced capabilities for configuring, previewing, and controlling events during animation playback. It extends Unity's default animation workflow, giving developers greater control over animation events and their integration.

With this system, events can be triggered from any state in your Animator Controller, including **Blend Trees**, allowing precise control over when events fire during animations. The system also features a customizable event receiver component that links animation events to UnityEvents, making event handling more flexible and maintainable.

## Key Features

- **Custom Animation Events**: Configure and trigger animation events at specific, normalized times (0 to 1) within an animation state.
- **Blend Tree Support**: Trigger events at the blend tree level, allowing precise event control when working with multiple animations.
- **Event Receiver Component**: A reusable component that enables animation events to trigger UnityEvents, streamlining and maintaining event handling.
- **Inspector Preview Mode (Scene View)**: Preview animations and blend trees *directly in the Scene view from the Unity Editor*, providing real-time visual feedback and dramatically improving workflow efficiency.
- **T-Pose Reset**: Quickly reset characters to their default T-pose, aiding in animation rigging and debugging.
- **StateMachineBehaviour Integration**: Uses `StateMachineBehaviour` to manage events tied to specific animation states.

This system offers enhanced flexibility and control over Unity’s Animation Event system, particularly valuable for managing complex animation events in both runtime and editor-time workflows.

## YouTube

[**Improved Animation Events in Unity**](https://youtu.be/XEDi7fUCQos?sub_confirmation=1)

You can also check out my [YouTube channel](https://www.youtube.com/@git-amend?sub_confirmation=1) for more Unity content.