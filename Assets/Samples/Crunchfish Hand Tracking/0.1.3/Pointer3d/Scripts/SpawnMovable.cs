/* Copyright (c) 2023 Crunchfish AB. All rights reserved. All information herein
 * is or may be trade secrets of Crunchfish AB.
 */
using UnityEngine;

namespace HandTracking {

  /// <summary>
  /// A class spawning a copy of the gameObject it is attached to with 
  /// a Movable in place of itself on the new object
  /// </summary>
  public class SpawnMovable : MonoBehaviour, Interactable {

    private ScaleHighlight highlight;
    private GameObject movableTemplate;
    void Start() {
      highlight = gameObject.AddComponent<ScaleHighlight>();
      movableTemplate = GameObject.Instantiate(gameObject, transform.position,  transform.rotation, transform.parent);
      Destroy(movableTemplate.GetComponent<SpawnMovable>());
      movableTemplate.AddComponent<Movable>();
      movableTemplate.SetActive(false);
    }

    public void Interact(InteractionState state, PointerHitInfo hitInfo, Ray ray, Hand hand, Pointer pointer) {
      if(null != highlight) 
        highlight.MaybeHighlight(state, hand);
      if (state == InteractionState.Activate) {
        var movable = GameObject.Instantiate(movableTemplate, transform.position + hitInfo.normal * 0.1f, transform.rotation, transform.parent);
        movable.SetActive(true);
        var movableScript = movable.GetComponent<Movable>();
        movableScript.highlight = movable.GetComponent<ScaleHighlight>();
        
        movableScript.deleteOnOpenToClosedHand = true;
        movableScript.highlight.Initialize(hand, highlight.scaleTarget);
        movableScript.highlight.baseScale = highlight.baseScale;
        movableScript.Interact(state, hitInfo, ray, hand, pointer);
      }
    }
  }
}