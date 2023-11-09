using UnityEngine;
using HandTracking;
public class PlaneCreationController : MonoBehaviour
{
    public HandednessType handedness;
    //public Transform rightHand;
    public GameObject canvasObject; // Assign the canvas object in the inspector
    private bool planeCreated = false;
    private bool calibrated = false;
    private Hand hand;
    private int cnt;
    private GameObject leftObject;
    void Start()
    {
        // Deactivate the canvas initially
        canvasObject.SetActive(false);
        hand = HandednessType.RIGHT_HAND == handedness ? TouchlessSession.instance.rightHand : TouchlessSession.instance.leftHand;
        cnt = 0;
        leftObject = GameObject.FindWithTag("left_tip");

  
    }

    void Update()
    {

        if (!planeCreated && hand.active && !calibrated)
        {
            // Calculate the position of the plane at the right hand's position

            Vector3 planePosition = leftObject.transform.position;
            //var palmDir = -hand.bones[SkeletalId.Wrist].up;
            //var rootForward = hand.root.forward;

            // Calculate the normal of the plane as the right hand's forward direction
            Vector3 planeNormal = -hand.bones[SkeletalId.Wrist].up;

            // Create the plane object based on the calculated position and normal
            GameObject plane = new GameObject("CustomPlane");
            plane.transform.position = planePosition;
            plane.transform.rotation = Quaternion.LookRotation(planeNormal);

            // Activate the canvas at the position of the calculated plane
            Debug.Log($"{planePosition},{planeNormal}");
            canvasObject.transform.position = planePosition;
            canvasObject.transform.rotation = Quaternion.identity;
            canvasObject.SetActive(true);

            // Instantiate the canvas prefab at the position of the calculated plane
            //Instantiate(canvasObject, planePosition, Quaternion.identity, plane.transform);


            // Set the canvas object's parent to the plane GameObject if needed
            //canvasObject.transform.SetParent(plane.transform);

            planeCreated = true;
            cnt += 1;
        }
        else if (planeCreated)
        {
            // Calculate the offset from 'keyboard' to 'hand'
            Vector3 offset = leftObject.transform.position - canvasObject.transform.position;
            Debug.Log(cnt);
            Debug.Log(offset + ", " + leftObject.transform.position + ", " + canvasObject.transform.position);
            Debug.Log(leftObject.transform.position);

            //Quaternion rotationOffset = Quaternion.Inverse(hand.rotation) * keyboard.rotation;

            // Apply the offset to 'keyboard'
            canvasObject.transform.position += offset;
            //keyboard.rotation = rotationOffset * keyboard.rotation;
            //calibrated = true;

        }
    }
}

