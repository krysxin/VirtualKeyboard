GesturePaint is a simpler sample, showing a configuration of the handtracking with a visualization of the hands. 
The Paint script shows how to use the hands gesturetype, finger positions and hand orientation to paint in the air and clear paints.


Two interactions are used in the sample:
* A continuous stream of CLOSED_PINCH is used together with a position between the tip of the index finger and thumb to draw. 
* A Gesture with a transition from OPEN_HAND to CLOSED_HAND within a time limit clears the screen of paintings.
 The poses also have a direction constraint requiring the hand to face the camera for the gesture to be recognized.