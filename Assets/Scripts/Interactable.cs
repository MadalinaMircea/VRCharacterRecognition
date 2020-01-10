using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hand activeHand;

    public Vector3 offset = Vector3.zero;
    //public Vector3 rotation = Vector3.zero;

    public void ApplyOffset(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = offset;
        transform.SetParent(null);
        //transform.eulerAngles = rotation;
    }
}
