using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HandTracking
{
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Button))]
    public class ButtonInteract : MonoBehaviour, MultiInteractable
    {
        private Text _textfield;
        private GameObject inputPreview;
        public string ButtonText;


        private float OutlineCutoffDistance = 0.07f;

        private Text _buttonText;

        public float InteractionCooldown = 1.0f;
        private Button _button;
        private float _cooldownTime;

        public ButtonType buttonType = ButtonType.Regular;
        private Shadow _outline;

        private PointerEventData _eventData;
        public enum ButtonType
        {
            Regular,
            Delete,
            Clear,

        }

        public void Start()
        {
            _textfield = GameObject.FindWithTag("TextField").GetComponent<Text>();
            inputPreview = GameObject.FindWithTag("InputText");

            _button = GetComponent<Button>();

            _buttonText = transform.GetChild(0).gameObject.GetComponent<Text>();
            switch (buttonType)
            {
                case ButtonType.Regular:
                    _buttonText.text = ButtonText;
                    break;
            }

            _outline = GetComponent<Outline>();
            _outline.effectColor = new Color(0, 0, 0, 1);
            _outline.effectDistance = Vector2.zero;

            // Dummy data to sent to event handler 
            _eventData = new PointerEventData(EventSystem.current);
        }

        public void Interact(InteractionState state, MultiPointerHitInfo hitInfo, Ray ray, Hand hand, MultiPointer pointer)
        {
            switch (state)
            {
                /* If finger moves away, remove outline and reset button state */
                case InteractionState.Exit:
                    _outline.effectDistance = Vector2.zero;
                    /* Sends signals to the button to stop being pressed and/or hovered */
                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerUpHandler);
                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerExitHandler);
                    break;

                /* When close enough, trigger button click once */
                case InteractionState.EnterActive:
                    switch (buttonType)
                    {
                        case ButtonType.Regular:
                            Destroy(inputPreview);
                            _textfield.text = _textfield.text + ButtonText;
                            break;

                        case ButtonType.Delete:
                            int currentTextLength = _textfield.text.Length;
                            if (currentTextLength > 0)
                            {
                                _textfield.text = _textfield.text.Substring(0, currentTextLength - 1);
                            }
                            else
                            {
                                _textfield.text = null;
                            }
                            Destroy(inputPreview);
                            break;

                        case ButtonType.Clear:
                            _textfield.text = null;
                            break;

                    }
                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerClickHandler);
                    break;

                /* If remaining in active state, keep button down */
                case InteractionState.Active:

                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerDownHandler);
                    break;

                /* If finger is in front of button, but not yet close enough to click */
                case InteractionState.EnterNeutral:

                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerUpHandler);
                    ExecuteEvents.Execute(gameObject, _eventData, ExecuteEvents.pointerEnterHandler);
                    break;

                /* If finger remains in previous mode */
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
                }
            }


        }



    }



}


