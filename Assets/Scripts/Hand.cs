using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction;

    private SteamVR_Behaviour_Pose pose;
    private FixedJoint joint;

    private Interactable currentInteractable;

    public List<Interactable> contactInteractables = new List<Interactable>();

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetStateDown(pose.inputSource))
        {
            Debug.Log("Down " + pose.inputSource);
            PickUp();
        }

        if (grabAction.GetStateUp(pose.inputSource))
        {
            Debug.Log("Up " + pose.inputSource);
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pen"))
        {
            contactInteractables.Add(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pen"))
        {
            contactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
        }
    }

    public void PickUp()
    {
        currentInteractable = GetNearestInteractable();

        if(currentInteractable != null)
        {
            if(currentInteractable.activeHand != null)
            {
                currentInteractable.activeHand.Drop();
            }

            currentInteractable.transform.position = transform.position;

            Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
            joint.connectedBody = targetBody;

            currentInteractable.activeHand = this;
        }
    }

    public void Drop()
    {
        if (currentInteractable != null)
        {
            Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
            targetBody.velocity = pose.GetVelocity();
            targetBody.angularVelocity = pose.GetAngularVelocity();

            joint.connectedBody = null;

            currentInteractable.activeHand = null;
            currentInteractable = null;
        }
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;

        float minDist = 100;
        float dist;

        foreach(Interactable interactable in contactInteractables)
        {
            dist = (interactable.transform.position - transform.position).sqrMagnitude;

            if(dist < minDist)
            {
                minDist = dist;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
