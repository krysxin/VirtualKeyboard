using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandTracking
{

  public class CursorPointer : Pointer
  {
    public int NumFingers = 1;
    public GameObject CursorPrefab;
    private GameObject[] _cursor;

    override protected void Start()
    {
      _cursor = new GameObject[NumFingers];
      base.Start();
    }

    override protected void Update()
    {
      base.Update();
    }

    Transform getCanvas(Transform element)
    {
      Canvas[] c = element.GetComponentsInChildren<Canvas>();
      if (c.Length == 0) return null;

      return c[0].gameObject.transform;
    }

    override protected void Interact(PointerHitInfo hitInfo)
    {
      if (hitInfo.hitTransform != null)
      {
        var canvas = getCanvas(hitInfo.hitTransform);
        if (_cursor[0] == null)
        {
          _cursor[0] = GameObject.Instantiate(CursorPrefab);
        }
        if (canvas != null)
        {
          _cursor[0].transform.SetParent(canvas);
          _cursor[0].transform.localScale = Vector3.one;
        }


        _cursor[0].transform.position = hitInfo.position + new Vector3(0, 0, -0.01f);

      }

      // Move cursor slightly in front of hit object

      base.Interact(hitInfo);
    }



  }

}

