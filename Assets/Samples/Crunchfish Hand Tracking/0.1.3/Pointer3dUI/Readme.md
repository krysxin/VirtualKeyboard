Pointer3dUI shows a pointer configured with a HandTrackingInput to simulate a mouse and interacting with UI elements built with components from the Unity UI or TextMeshPro packages.

Two interactions are used in this sample:
* With the pointer pointing at the UI, performing a CLOSED_PINCH simulates a mouse down event at the position pointed up. a change from CLOSED_PINCH to any other simulates a mouse up event.
* There is only one pointer since we are simulating mouse input. To switch what hand the pointer is attached to the hand without the pointer can perform two pinches in succession to take controll of the pointer.