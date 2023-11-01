using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HandTracking
{
    public class ButtonInteract : MonoBehaviour, MultiInteractable
    {
        private Text _uiText;
        public string ButtonText;
        private GameObject _buttonText;
        private GameObject[] fingerTips;
        private InteractionState _pastState = InteractionState.Neutral;

        public AudioSource clickSound;

        public float InteractionCooldown = 1.0f;
        [SerializeField]
        private bool canInteract = true; // Flag to track whether interaction is allowed
        private Button button;
        private ColorBlock colorBlock;
        private Color pressedColor;
        private float _cooldownTime;

        public void Start()
        {
            _uiText = GameObject.FindWithTag("TextField").GetComponent<Text>();
            _buttonText = transform.GetChild(0).gameObject;
            _buttonText.GetComponent<Text>().text = ButtonText;
            fingerTips = GameObject.FindGameObjectsWithTag("tips");
            button = gameObject.GetComponent<Button>();
            pressedColor = new Color32(165, 171, 211, 255);
        }

        public void Update()
        {
            
            if(canInteract == false && _cooldownTime > InteractionCooldown)
            {
                colorBlock.normalColor = Color.white;
                button.colors = colorBlock;

                // Reset the flag to allow interaction again
                canInteract = true;
            }
            _cooldownTime += Time.deltaTime;
        }

        public void Interact(InteractionState state, MultiPointerHitInfo hitInfo, Ray ray, Hand hand, MultiPointer pointer)
        {
             
                    if (canInteract && state == InteractionState.EnterActive)
                    {
                        _uiText.text = _uiText.text + ButtonText;
                        //Play sound
                        clickSound.Play();

                        //Change color
                        colorBlock = button.colors;
                        colorBlock.normalColor = pressedColor;
                        button.colors = colorBlock;

                        // Set canInteract to false to prevent further interactions
                        canInteract = false;

                        _cooldownTime = 0;
                    }
                    if (state != _pastState)
                    {
                        _pastState = state;
                    }
                    return;
                
            

            
        }

    }

    

}


