/* Copyright (c) 2023 Crunchfish AB. All rights reserved. All information herein
 * is or may be trade secrets of Crunchfish AB.
 */
using UnityEngine;

namespace HandTracking {
  public class SetColorAndSpawnMovables : MonoBehaviour {
    public Color[] colors;
    public Renderer[] renderers;
    void Start() {
      Debug.Assert(colors.Length == renderers.Length);
      for (int i = 0; i < colors.Length; i++) {
        renderers[i].material.color = colors[i];
        renderers[i].gameObject.AddComponent<SpawnMovable>();
      }
      Destroy(this);
    }
  }
}