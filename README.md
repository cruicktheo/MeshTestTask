
# MeshTestTask

The goal of this task is to create two procedural meshs that can be manipulated in runtime in various ways including rotation, color by rotation, lissajous animation and mesh deformation. 

The project was created in Unity 2022.3.33f1, and targets the Oculus Quest. Development was done using the Quest 3, however it should still run on other Quest devices, however this is untested. Also the controller models in the project are the Quest Touch Plus (Quest 3) controllers.

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/MeshAnimationDemonstration.GIF)

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/AllDemonstration.GIF)
## UI

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/ObjectAttractorDemonstration.GIF)

Both objects can be attracted to the controllers by holding the trigger. Releasing the trigger will let go of the object and it will return to its position.

The visualisations listed later can all be toggled, and also adjusted in the case of Lissajous Animation. The UI panel can be access by looking towards the floor. The UI panel should slide up showing buttons which will toggle the neccessary visualisations on and off. The controllers ray will become visible when hovering over the UI panel, and the buttons can be selected with the controller trigger. The UI panel can be dismissed again by looking back up.
## Object Rotator

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/ObjectRotatorDemonstration.GIF)

Selecting the object rotator visualisation makes the first object face the second at all times, including during other animations and when attracted to the user controller.
## Rotation Coloriser

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/RotationalColorisrDemonstration.GIF)

The rotational coloriser colors the first object based on where the second object is in relation to its forward vector. It is fully red when facing, fully blue then the second obect is behind, and purple in the half way point.
## Lissajous Animation

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/LissajousAnimatorDemonstration.GIF)

Selecting the lissajous animation option makes the first object float on x and y in a lissajous manner. Selecting this option enabled a second UI panel where the variables of the lissajous formula can be manipulated and the results viewed in real time.
## Mesh Animation

![](https://github.com/cruicktheo/MeshTestTask/blob/main/DemonstrationGifs/MeshAnimationDemonstration.GIF)

Selecting the mesh animation option makes the first objects mesh deform using perlin noise over time. Note: when this object is attracted to the left controller the animation will stop and resume once released.