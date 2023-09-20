using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandTracking
{

  public class CursorPointer : Pointer
  {
    public int NumFingers = 1;
    public GameObject CursorPrefab;
    public Material CursorMaterial;
    private GameObject[] _cursor;

    override protected void Start()
    {
      _cursor = new GameObject[NumFingers];
      _cursor[0] = GameObject.Instantiate(CursorPrefab);
      _cursor[0].SetActive(false);
      base.Start();
    }

    override protected void Update()
    {
      _cursor[0].SetActive(base.pointerActive);
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

      if (hitInfo.hitTransform != null)
      {
        var canvas = getCanvas(hitInfo.hitTransform);
        if (canvas != null)
        {
          _cursor[0].transform.SetParent(canvas);
          _cursor[0].transform.localScale = Vector3.one;
        }


      }

      if (hitInfo.validHit)
      {
        _cursor[0].transform.position = hitInfo.position + new Vector3(0, 0, -0.01f);
        CursorMaterial.SetFloat("_Distance", hitInfo.rayDistance);
      }



      base.Interact(hitInfo);
    }



  }

}

