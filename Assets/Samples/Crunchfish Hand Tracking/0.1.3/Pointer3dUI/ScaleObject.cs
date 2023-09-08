/* Copyright (c) 2023 Crunchfish AB. All rights reserved. All information herein
 * is or may be trade secrets of Crunchfish AB.
 */
using UnityEngine;

namespace HandTracking {
  public class ScaleObject : MonoBehaviour, Interactable {
    public Transform toScale;
    public Vector3 scaleTarget = new Vector3(1.5f, 1.5f, 1.5f);
    ScaleHighlight highlight;
    void Start() {
      highlight = gameObject.AddComponent<ScaleHighlight>();
      highlight.scaleTarget = scaleTarget;
      highlight.toScale = null == toScale ? transform : toScale;
      highlight.keepHighlightWhilePinching = true;
    }

    public void Interact(InteractionState state, PointerHitInfo hitInfo, Ray ray, Hand hand, Pointer pointer) {
      highlight.MaybeHighlight(state, hand);
    }
  }
}