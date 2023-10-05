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
    

    private Passcode passcodeScript;
    private Transform childText;
    private string keyName;
    private bool canInteract = true; // Flag to track whether interaction is allowed

  

    // Start is called before the first frame update
    void Start()
    {

        passcodeScript = GetComponentInParent<Passcode>();
        childText = transform.Find("Text");
        keyName = childText.GetComponent<Text>().text;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceleft = Vector3.Distance(transform.position, fingerTipLeft.position);
        float distanceright = Vector3.Distance(transform.position, fingerTipRight.position);

        if ((distanceleft < interactionThreshold || distanceright < interactionThreshold) && canInteract)
        {
            // Execute CodeFunction when within the interaction threshold
            if (keyName == "DEL")
            {
                passcodeScript.Delete();
            }
            else
            {
                passcodeScript.CodeFunction(num); // Replace "Character" with the character you want to input
            }

            // Set canInteract to false to prevent further interactions
            canInteract = false;

            // Start a coroutine to reset canInteract after a delay
            StartCoroutine(ResetInteractionCooldown(0.5f)); // the cooldown time in seconds
        }
    }

    // Coroutine to reset the interaction cooldown
    private IEnumerator ResetInteractionCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);

        // Reset the flag to allow interaction again
        canInteract = true;
    }

    // Get text of the key
    //private string GetKeyName()
    //{
    //    Text text
    //}
}
