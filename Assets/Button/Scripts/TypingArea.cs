using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HandTracking;

public class TypingArea : MonoBehaviour
{
    public GameObject left_hand;
    public GameObject right_hand;
    public GameObject leftTypingHand;
    public GameObject righTypingtHand;

    public HandednessType handedness;

    private Hand hand;

  
    //private void OnTriggerEnter(Collider other)
    //{
    //    hand = HandednessType.RIGHT_HAND == handedness ? TouchlessSession.instance.rightHand : TouchlessSession.instance.leftHand;
    //    if (hand.active)
    //    {
    //        Console.WriteLine("Hand detected in the interactive area");
    //        //leftTypingHand.SetActive(true);

    //    }

    //}


}
