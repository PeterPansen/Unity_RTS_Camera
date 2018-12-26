# Unity_RTS_Camera
Provides a simple script with full RTS-Camera functionality for Unity. The camera uses rigidbody physics to move and accelerate/decelerate.

## Controls ##

- WASD movement along x- and z-Axis
- ScrollWheel used to zoom in and out
- Q and E keys turn the camera on point

## Parameters ##

Movement
- Move Speed = Force applied to our camera-rigidbody. Higher values give a more direct and stronger movement impulse
- Drag = Overall loss in movement speed over time. Describes how fast the camera decelerates
- Mass = Weight of the camera-rigidbody. Affects speed loss and the power needed to move the camera

Rotation
- Turn Speed = Force applied to rigidbody torgue. Defines how fast the camera can turn on point
- Angular_drag = The loss of rotation speed over time

Restrictions
- Min / Max Height = The minimal and maximal value the camera is allowed on the y-Axis

Zoom
- Zoom Speed = Defines how fast the camera reaches the target zoom level 
- Zoom Drag = Loss of movementum on camera zoom
