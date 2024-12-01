# A Prototype for Enhancing Palpation and Percussion Training for Physical Examination via Augmented Reality

This repository contains the assets of Unity scene used to code the Meta Quest II headset.

Author: Pierre Desbruns


## Reference scene
Reference scene is VisualRendering (`Assets/Scenes/VisualRendering.unity`). It is organised as follows:

![Screenshot of VisualRendering scene hierarchy in Unity editor](screenshot_unity_scene)

## Game objects
- *OVRCameraRig* produces main headset features, such as student hand tracking and world environment rendering with Meta Quest Passthrough. This object is a prefab from Meta Quest libraries.
- *DataReceiver* is responsible for UDP connection with server to receive data from. This object has no visual component, it is only a script running in background.
- *ForceIndicators* contains the 3D rectangles used to render force on fingertips.
- *UI* represents the 2D layout containing timer (*Timestamp* object) that user has constantly in front of him/her. This is a Meta Quest prefab based on Unity built-in canvasses that complies with \gls{ar} development.
- *Hand* corresponds to the 3D model of doctor's hand, downloaded from the Internet.

## C# scripts
- [`DataReceiver.cs`](Assets/Scripts/DataReceiver.cs): receive data from server via UDP. It is attached to *DataReceiver* object.
- [`IndicatorsUpdater.cs`](Assets/Scripts/IndicatorsUpdater.cs): update 3D rectangles' color and height according to received force values. It is attached to *ForceIndicators* object.
- [`TimestampUpdater.cs`](Assets/Scripts/TimestampUpdater.cs): update timer according to received timestamp. It is attached to *Timestamp* object, child of *UI*.
- [`UIPositionUpdater.cs`](Assets/Scripts/UIPositionUpdater.cs): update UI position containing timer to headset's camera, so that user always has it in front of him/her. It is attached to *UI* object.

## Useful links
- [Set up Unity for XR development](https://developers.meta.com/horizon/documentation/unity/unity-project-setup)
- [Hello world Unity project for XR development](https://developers.meta.com/horizon/documentation/unity/unity-tutorial-hello-vr)
- [Hand tracking tutorial](https://developers.meta.com/horizon/documentation/unity/unity-tutorial-basic-hand-tracking/)
- [Making 2D user interfaces based on Unity canvasses](https://developers.meta.com/horizon/blog/unitys-ui-system-in-vr/)