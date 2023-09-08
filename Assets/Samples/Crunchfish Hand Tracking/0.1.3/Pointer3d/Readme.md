The Pointer3d sample shows how to setup the Pointer class and use it with the Interactable interface.

Two different configurations of the pointer is displayed in different scenes. 
* Pointer3dCamera points into the scene from the perspective of the camera giving a more direct conection between the hand and what is interacted with
* Pointer3dRay Points into the scene from a lower angle to allow for better ergonomics where the hand can be kept lower. The offset in the example is a starting point but might need adjustment to fit the device and use case.

The used Interactable's are MonoBehaviours with attatched collider and ScaleHighlight behaviour.

Two interactions are used in this sample:
* With the pointer pointing at a box performing a CLOSED_PINCH activates and grabs a box.
* A Gesture with a transition from OPEN_HAND to CLOSED_HAND within a time limit is used to remove any boxes instantiated during the session.
 The poses also have a direction constraint requiring the hand to face the camera for the gesture to be recognized.