using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Passcode : MonoBehaviour
{
    string Code = "456";
    string Nr = null;
    int NrIndex = 0;
    string alpha;
    public Text UiText = null;
    
    public void CodeFunction(string Numbers)
    {
        NrIndex++;
        Nr = Nr + Numbers;
        UiText.text = Nr;

    }
    public void Enter()
    {
        if (Nr == Code)
        {
            SceneManager.LoadScene(1);
            Debug.Log("Matched");
        }
    }
    public void Delete()
    {
        int Nrlength = Nr.Length;
        NrIndex++;
        if(Nrlength > 0)
        {
            Nr = Nr.Substring(0, Nrlength - 1); 
        }
        else
        {
            Nr = null;
        }

        UiText.text = Nr;
    }
}