using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FingerButtonInteraction : MonoBehaviour
{
    public Transform fingerTipLeft;
    public Transform fingerTipRight;
    public float interactionThreshold = 1.0f;
    public string num;
    public AudioSource clickSound;

    private Passcode passcodeScript;
    private Transform childText;
    private string keyName;
    private bool canInteract = true; // Flag to track whether interaction is allowed
    private Button button;
    private ColorBlock colorBlock;
    private Color pressedColor; 




    // Start is called before the first frame update
    void Start()
    {

        passcodeScript = GetComponentInParent<Passcode>();
        childText = transform.Find("Text");
        keyName = childText.GetComponent<Text>().text;
        button = gameObject.GetComponent<Button>();
        pressedColor = new Color32(165, 171, 211, 255);
        //fingerTipLeft = fingerTipLeft.transform;
        //fingerTipRight = GameObject.FindWithTag("right_tip").transform;
        Debug.Log(fingerTipLeft.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        float distanceleft = Vector3.Distance(transform.position, fingerTipLeft.position);
        float distanceright = Vector3.Distance(transform.position, fingerTipRight.position);

        if ((distanceleft < interactionThreshold || distanceright < interactionThreshold) && canInteract)
        {
            // Execute CodeFunction when within the interaction threshold
            if (keyName == "DEL")
            {
                passcodeScript.Delete();
            }
            else if (keyName == "Clear")
            {
                passcodeScript.Clear();
            }
            else
            {
                passcodeScript.CodeFunction(num); // Replace "Character" with the character you want to input

            }

            //Play sound
            clickSound.Play();

            //Change color
            colorBlock = button.colors;
            colorBlock.normalColor = pressedColor;
            button.colors = colorBlock;

            // Set canInteract to false to prevent further interactions
            canInteract = false;

            // Start a coroutine to reset canInteract after a delay
            StartCoroutine(ResetInteractionCooldown(0.8f)); // the cooldown time in seconds
        }
    }

    // Coroutine to reset the interaction cooldown
    private IEnumerator ResetInteractionCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);

        colorBlock.normalColor = Color.white;
        button.colors = colorBlock;

        // Reset the flag to allow interaction again
        canInteract = true;
    }

    // Get text of the key
    //private string GetKeyName()
    //{
    //    Text text
    //}
    
}
