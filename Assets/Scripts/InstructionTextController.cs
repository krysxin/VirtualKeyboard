using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InstructionTextController : MonoBehaviour
{
    public Text instructionText;
    public float instructionDuration = 5.0f; // Duration in seconds
    public GameObject planeCreationController;

    void Start()
    {
        StartCoroutine(DisplayInstruction());
    }

    IEnumerator DisplayInstruction()
    {
        //instructionText.text = "Please place your both hands on a fixed surface for " + instructionDuration + " seconds";
        //yield return new WaitForSeconds(instructionDuration);
        //instructionText.text = "Hands detected!";
        planeCreationController.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        //gameObject.SetActive(false); // Disable this script after the instruction is shown.
    }
}
