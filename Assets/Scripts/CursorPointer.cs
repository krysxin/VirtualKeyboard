using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandTracking
{

  public class CursorPointer : Pointer
  {
    public GameObject CursorPrefab;
    public Material CursorMaterial;
    private GameObject[] _cursors;

    override protected void Start()
    {
      _cursors = new GameObject[5];
      for (int i = 0; i < 5; i++)
      {
        _cursors[i] = GameObject.Instantiate(CursorPrefab);
        _cursors[i].SetActive(false);
      }

      base.Start();
    }

    override protected void Update()
    {
      for (int i = 0; i < 5; i++)
      {
        _cursors[i].SetActive(base.pointerActive);
      }

      base.Update();
    }

    Transform getCanvas(Transform element)
    {
      Transform result = null;
      Canvas c = element.GetComponentInChildren<Canvas>();
      if (c != null)
      {
        result = c.gameObject.transform;
      }
      c = element.GetComponentInParent<Canvas>();
      if (c != null)
      {
        Debug.Log(c.gameObject);
        result = c.gameObject.transform;
      }

      return result;
    }

    override protected void Interact(PointerHitInfo hitInfo)
    {
      int fingerIndex = hitInfo.fingerIndex;
      if (hitInfo.hitTransform != null)
      {
        var canvas = getCanvas(hitInfo.hitTransform);
        if (canvas != null)
        {
          _cursors[fingerIndex].transform.SetParent(canvas);
          _cursors[fingerIndex].transform.localScale = Vector3.one;
        }


      }

      if (hitInfo.validHit)
      {
        _cursors[fingerIndex].transform.position = hitInfo.position + new Vector3(0, 0, -0.01f);
        CursorMaterial.SetFloat("_Distance", hitInfo.rayDistance);
      }



      base.Interact(hitInfo);
    }



  }

}

