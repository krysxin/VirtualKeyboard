using UnityEngine;
using UnityEngine.UI;

public class FingerInteraction : MonoBehaviour
{
    private bool isMoving = false;
    private Button targetButton; // The button to interact with
    private InputField inputField; // The text input field

    void Update()
    {
        if (isMoving)
        {
            // Move the finger towards the target button's position
            Vector3 targetPosition = targetButton.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2f);

            // Check if the finger has reached the button
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                InputTextOnButton();
            }
        }
    }

    // Function to initiate interaction with a button
    public void InteractWithButton(Button button)
    {
        isMoving = true;
        targetButton = button;
    }

    // Function to input text into the button's text field
    private void InputTextOnButton()
    {
        if (targetButton != null)
        {
            inputField = targetButton.GetComponentInChildren<InputField>();
            if (inputField != null)
            {
                // Replace the text in the input field with your desired text
                inputField.text = "Your Text Here";
            }
        }
    }
}
