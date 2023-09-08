/* Copyright (c) 2022 Crunchfish AB. All rights reserved. All information herein
 * is or may be trade secrets of Crunchfish AB.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HandTracking;

/// <summary>
///  Paint is an example of some simple interactions.
/// </summary>
/// <remarks>
/// Two interactions are used:
/// A continuous stream of CLOSED_PINCH is used together with a position between 
/// the tip of the index finger and thumb to draw. 
/// The resolution of a drawing step is configured to a minimumDistance
/// by only updating when the change in position is that big.
/// A Gesture with a transition from OPEN_HAND to CLOSED_HAND within a time limit clears the screen of paintings.
/// The poses also have a direction constraint requiring the hand to face the camera for the gesture to be recognized.
/// </remarks>
public class Paint : MonoBehaviour {
  /// <summary>
  /// What hand should be referenced
  /// </summary>
  public HandednessType handedness;
  /// <summary>
  /// Distance to move hand before drawing is triggered. This is effectively the resolution of the painting
  /// </summary>
  public float minimumDistance = 0.003f;
  /// <summary>
  /// Has to be scalable and a renderer in a childObject
  /// </summary>
  public GameObject PaintObjectPrefab = null;
  /// <summary>
  /// Common object for multiple instances to be able to clear all paintings
  /// </summary>
  /// <typeparam name="GameObject"></typeparam>
  /// <returns></returns>
  private static List<GameObject> paintings = new List<GameObject>();
  /// <summary>
  /// Randomized and used for drawing
  /// </summary>
  private Color baseColor;
  /// <summary>
  /// One continuous line of positions to spawn PaintObject on
  /// </summary>
  private List<Vector3> paintPositions;
  /// <summary>
  /// Recreated and used as parent for each sequence of paintObjects and then added to paintings
  /// </summary>
  private GameObject container;
  /// <summary>
  /// The OPEN_HAND part of a "clear" gesture happend at this time
  /// </summary>
  private float removeReadyTime;
  /// <summary>
  /// The CLOSED_HAND part of a clear gesture has to be before removeReadyTime + gestureDuration
  /// </summary>
  private readonly float gestureDuration = 0.3f;
  /// <summary>
  /// The referenced hand
  /// </summary>
  private Hand hand;
  void Start() {
    hand = HandednessType.RIGHT_HAND == handedness ? TouchlessSession.instance.rightHand : TouchlessSession.instance.leftHand;
    paintPositions = new List<Vector3>();
    container = new GameObject();
  }
  /// <summary>
  /// Manage painting based on hand status
  /// </summary>
  void Update() {
    if (!hand.active) {
      StopPaint();
      return;
    }
    if (GestureType.CLOSED_PINCH == hand.type) {
      //Calculate an interaction position that looks correct and is stable when when opening a Closed pinch
      var index = hand.bones[SkeletalId.IndexTip].position;
      var thumb = hand.bones[SkeletalId.ThumbTip].position;
      var thumbToIndexInterpolationFraction = 0.3f;
      var position = thumb + (index - thumb) * thumbToIndexInterpolationFraction;
      PaintToPos(position);
    }
    else {
      StopPaint();
    }
    var palmDir = -hand.bones[SkeletalId.Wrist].up;
    var rootForward = hand.root.forward;
    float palmDirNegatedForwardAlignmentRequirement = -0.5f;
    //If the palm is less aligned with forward than required the clear gesture is not allowed to trigger
    //This constraint makes the gesture more distinct and harder to trigger accidentally
    if (Vector3.Dot(palmDir, rootForward) > palmDirNegatedForwardAlignmentRequirement) return;
    if (GestureType.CLOSED_HAND == hand.type) {
      if (Time.time - removeReadyTime < gestureDuration) {
        //Gesture detected, clear all paintings
        foreach (var obj in paintings) {
          DestroyImmediate(obj);
        }
        paintings.Clear();
      }
    }
    else if (GestureType.OPEN_HAND == hand.type) {
      removeReadyTime = Time.time;
    }
  }

  /// <summary>
  /// Initialize new paintPositions sequence and handle active painting to a position
  /// </summary>
  /// <param name="targetPosition">The position to paint to</param>
  public void PaintToPos(Vector3 targetPosition) {
    if (paintPositions.Count == 0) {
      baseColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
      paintPositions.Add(targetPosition);
      return;
    }
    Vector3 dir = targetPosition - paintPositions[paintPositions.Count - 1];
    if (dir.magnitude < minimumDistance) {
      return;
    }
    var distanceLeft = dir.magnitude;
    dir.Normalize();
    float stepsize;
    //Constrain the max length of the paint objects and randomize it to make it look more interesting
    while (distanceLeft > 0f) {
      float maxStepDistanceMultiplier = 1.2f;
      float minStepDistanceMultiplier = 0.8f;
      if (distanceLeft < minimumDistance * maxStepDistanceMultiplier) {
        stepsize = distanceLeft;//Makes sure we hit the targetPosition
        distanceLeft = 0f;
      }
      else {
        stepsize = minimumDistance * Random.Range(minStepDistanceMultiplier, maxStepDistanceMultiplier);
        distanceLeft -= stepsize;
      }
      Vector3 position = paintPositions[paintPositions.Count - 1] + dir * stepsize;

      paintPositions.Add(position);
      var paintObject = Instantiate(PaintObjectPrefab, position, Quaternion.identity, container.transform).transform;
      var paintDiameter = minimumDistance * 2f;
      paintObject.localScale = new Vector3(paintDiameter, stepsize, paintDiameter);
      paintObject.up = dir;
      
      //randomize color brightness for a dynamic look
      float h, s, v;
      Color.RGBToHSV(baseColor, out h, out s, out v);
      float valueVariance = 0.3f;
      v *= Random.Range(1f-valueVariance, 1f+valueVariance);
      paintObject.GetComponentInChildren<Renderer>().material.color = Color.HSVToRGB(h, s, v);
    }
  }

  /// <summary>
  /// Stop any active painting going on
  /// </summary>
  public void StopPaint() {
    if (paintPositions.Count == 0) {
      return;
    }
    paintPositions.Clear();
    paintings.Add(container);
    container = new GameObject();
  }
}
