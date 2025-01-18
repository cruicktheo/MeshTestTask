
# MeshTestTask

The goal of this task is to create two procedural meshs that can be manipulated in runtime in various ways including rotation, color by rotation, lissajous animation and mesh deformation. 

The project was created in Unity 2022.3.33f1, and targets the Oculus Quest. Development was done using the Quest 3, however it should still run on other Quest devices, however this is untested. Also the controller models in the project are the Quest Touch Plus (Quest 3) controllers.

The project can be built using the Unity build system without any changes. Ensure the MeshVisualiser.unity Scene is included in the build scenes. A built APK with the latest changes exists in the repo for easy install.

The following demonstration gifs include the OVR profiler overlay to show performance at 90hz.

## UI

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/ObjectAttractorDemonstration.GIF)

Both objects can be attracted to the controllers by holding the trigger. Releasing the trigger will let go of the object and it will return to its position.

The visualisations listed later can all be toggled, and also adjusted in the case of Lissajous Animation. The UI panel can be accessed by looking towards the floor. The UI panel should slide up showing buttons which will toggle the neccessary visualisations on and off. The controller rays will become visible when hovering over the UI panel, and the buttons can be selected with the controller trigger. The UI panel can be dismissed again by looking back up.

## Mesh Creation

The mesh generator creates simple spheres, taking in an optional parameter to create a cone indicator on the front. This is created as a separate mesh.

## Object Rotator

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/ObjectRotatorDemonstration.GIF)

Selecting the object rotator visualisation makes the first object face the second at all times, including during other animations and when attracted to the user controller.

## Rotation Coloriser

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/RotationalColorisrDemonstration.GIF)

The rotational coloriser colors the first object based on where the second object is in relation to its forward vector. It is fully red when facing, fully blue then the second object is behind, and purple in the half way point.

## Lissajous Animation

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/LissajousAnimatorDemonstration.GIF)

Selecting the lissajous animation option makes the first object float on x and y in a lissajous manner. Selecting this option enables a second UI panel where the variables of the lissajous formula can be manipulated and the results viewed in real time. Note: when this object is attracted to the left controller the animation will stop and resume once released.

## Mesh Animation

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/MeshAnimationDemonstration.GIF)

Selecting the mesh animation option makes the first object's mesh deform using perlin noise over time.

## Combined
![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/AllDemonstration.GIF)

Demonstrated here are all options toggled together, showing combined functionality. Note: the demonstration gifs were taken before a bug was fixed causing the indication cone to show its inner face, and has been since fixed.

## Considerations

It made the most sense for this project to have the UI dismiss itself when not in use, to allow full inspection of the visuals. This was also the reason for the decision to shorten the controller rays when they are not pointing at UI.

When creating the mesh deformation animation, I considered using the burst compiler to create parallel jobs or use a compute shader to make this more performant, but found the performance to be fine on the headset at 90hz. If the meshes were more complex, or there were more of them I would have taken this approach.

The decision was made to not have the visualisations be MonoBehaviours and rather classes that can be updated from a central script on the created mesh object. This seemed like a nicely extensible approach.

The decision was made to exclude the cone in the mesh deformation animation for clarity purposes.