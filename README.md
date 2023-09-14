# VirtualKeyboard
A project for hand gesture-based virtual keyboard in AR
GIT branch name: keyboard
Keyboard that appears on a given surface (desktop)  
Surface detection
Small scale sample scene (e.g., two buttons)
Use collider to detect “touch”
      GIT branch name: finger-tracking 
Hand tracking that highlight keys that are close to being pressed, brightness based on proximity.
Fingers cast shadows on keyboard to track movement between keys
Requires hand tracking/finger transform
Start with one finger, find closest point on key with vector length as “multiplier”
Keys hover slightly from keyboard, cast drop shadows to increase feeling of depth
      GIT branch name: interaction 
Clicking sound and a small outline when a button is pressed
Add cursor and floating text area to display typed letters
Requires no hand tracking to implement
Receive the output from the yellow part
