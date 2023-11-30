using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyboardCalibration : MonoBehaviour
{
    public GameObject canvasObject; // Reference to the Canvas game object
    public float calibrationDuration = 5.0f; // Calibration duration in seconds
    public Transform leftHand; // Reference to the left hand GameObject
    public Transform rightHand; // Reference to the right hand GameObject
    private Vector3 offset;

    private bool calibrationComplete = false;

    void Start()
    {
        // Display calibration instruction
        Debug.Log("Please place both hands on a flat surface for 5 seconds.");

        // Start the calibration coroutine
        StartCoroutine(CalibrateKeyboard());
    }

    IEnumerator CalibrateKeyboard()
    {
        // Deactivate the Canvas during calibration
        canvasObject.SetActive(false);

        yield return new WaitForSeconds(calibrationDuration);

        // Get the positions of the left and right hands
        Vector3 leftPos = leftHand.position;
        Vector3 rightPos = rightHand.position;

        // Calculate the center point of the hands as the keyboard position
        Vector3 kbPos = (leftPos + rightPos) / 2f;
        offset = kbPos - canvasObject.transform.position;
        Vector3 kbPos2 = kbPos + new Vector3 (0.0f, 0.0f, offset.z + 0.2f);
        Debug.Log(kbPos + " " + kbPos2);

        // Set the position of the Canvas game object based on the keyboard position
        canvasObject.transform.position = kbPos2;

        // Activate the Canvas after calibration
        canvasObject.SetActive(true);

        // Calibration complete
        calibrationComplete = true;

        Debug.Log("Calibration complete. Keyboard positioned at: " + kbPos);
    }

}
