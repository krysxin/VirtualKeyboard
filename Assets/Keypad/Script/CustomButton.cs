using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public Transform lefthand;

    //public delegate void CustomAction();
    //public CustomAction customAction;
    public System.Action customAction;

    void Update()
    {
        bool isHovering = RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), lefthand.position);
        if (isHovering && customAction != null)
        {
            customAction();
        }
    }

}


