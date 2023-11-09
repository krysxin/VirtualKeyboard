using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HandTracking
{
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(AudioSource))]
    public class ButtonInteract : MonoBehaviour, MultiInteractable
    {
        private Text _uiText;
        public string ButtonText;

        private float OutlineCutoffDistance = 0.07f;

        private GameObject _buttonText;
        private GameObject[] fingerTips;
        private InteractionState _pastState = InteractionState.Neutral;

        public AudioSource clickSound;

        public float InteractionCooldown = 1.0f;
        [SerializeField]
        private bool canInteract = true; // Flag to track whether interaction is allowed
        private Button _button;
        private ColorBlock _colorBlock;
        private Color _pressedColor;
        private float _cooldownTime;

        private Shadow _outline;

        private PointerEventData _eventData;

        public void Start()
        {
            _uiText = GameObject.FindWithTag("TextField").GetComponent<Text>();

            _button = GetComponent<Button>();
            _colorBlock = _button.colors;

            _buttonText = transform.GetChild(0).gameObject;
            _buttonText.GetComponent<Text>().text = ButtonText;

            _outline = GetComponent<Outline>();
            _outline.effectColor = new Color(0, 0, 0, 1);
            _outline.effectDistance = Vector2.zero;

            // Dummy data to sent to the event handler 
            _eventData = new PointerEventData(EventSystem.current);
        }

        public void Interact(InteractionState state, MultiPointerHitInfo hitInfo, Ray ray, Hand hand, MultiPointer pointer)
        {
            switch (state)
            {
                case InteractionState.Exit:
                    _outline.effectDistance = Vector2.zero;

                    /* Sends signals to the button to stop being pressed and/or hovered */
                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerUpHandler);
                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerExitHandler);
                    canInteract = true;
                    break;

                case InteractionState.EnterActive:
                    if (canInteract)
                    {

                        _uiText.text = _uiText.text + ButtonText;
                        //Play sound
                        clickSound.Play();

                        ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerDownHandler);
                        // Set canInteract to false to prevent further interactions
                        canInteract = false;

                    }
                    break;


                case InteractionState.EnterNeutral:

                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerUpHandler);
                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerEnterHandler);
                    canInteract = true;
                    break;

                case InteractionState.Neutral:
                    break;
            }
            if (hitInfo != null)
            {

                float dist = hitInfo.rayDistance;
                if (dist > OutlineCutoffDistance)
                {
                    _outline.effectDistance = Vector2.zero;
                }
                else
                {
                    // TODO: Replace 100 with variable
                    dist = 5 - dist * 100;
                    _outline.effectDistance = new Vector2(dist, dist);
                    //_outline.effectColor *= 255 - hitInfo.rayDistance * 255;
                }
            }


            if (state != _pastState)
            {
                _pastState = state;
            }
            return;




        }



    }



}


