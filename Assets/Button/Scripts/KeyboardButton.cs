using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardButton : MonoBehaviour
{
    ARKeyboard keyboard;
    TextMeshProUGUI buttonText;


    // Start is called before the first frame update
    void Start()
    {
        keyboard = GetComponentInParent<ARKeyboard>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text.Length == 1)
        {
            NameToButtonText();
            GetComponentInChildren<ButtonVR>().onRelease.AddListener(delegate { keyboard.InsertChar(buttonText.text); });
        }
    }

    public void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }

   
}
