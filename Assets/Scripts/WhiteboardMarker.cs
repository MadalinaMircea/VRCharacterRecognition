using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WhiteboardMarker : MonoBehaviour
{
    public Whiteboard whiteboard;

    private RaycastHit touch;

    //private bool lastTouch;
    //private Quaternion lastAngle;

    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    //private void Update()
    //{
    //    //if(interactable.activeHand != null)
    //    //{
    //    //    if(interactable.activeHand.createPenAction.GetStateDown(interactable.activeHand.pose.inputSource))
    //    //    {
    //            Transform blackTip = transform.Find("BlackPart");
    //            float blackHeight = blackTip.transform.localScale.y;
    //            Debug.Log(blackHeight);
    //            Vector3 blackPos = blackTip.transform.localPosition;

    //            if (Physics.Raycast(blackPos, -blackTip.up, out touch, blackHeight))
    //            {
    //                if (touch.collider.CompareTag("Whiteboard"))
    //                {
    //                    whiteboard = touch.collider.GetComponent<Whiteboard>();
    //                    whiteboard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
    //                    whiteboard.SetTouch(true);

    //                    if (!lastTouch)
    //                    {
    //                        lastTouch = true;
    //                        lastAngle = transform.rotation;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                whiteboard.SetTouch(false);
    //                lastTouch = false;
    //            }
    //    //    }
    //    //}

    //    if (lastTouch)
    //    {
    //        transform.rotation = lastAngle;
    //    }
    //}

    private void Update()
    {
        if (interactable.activeHand != null)
        {
            Transform blackTip = transform.Find("BlackPart");
            float blackHeight = blackTip.transform.localScale.y / 4;
            Vector3 blackPos = blackTip.transform.position;

            if (Physics.Raycast(blackPos, -blackTip.up, out touch, blackHeight))
            {
                //Debug.Log(touch.collider.tag);
                if (touch.collider.CompareTag("Whiteboard"))
                {
                    whiteboard = touch.collider.GetComponent<Whiteboard>();
                    whiteboard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
                    whiteboard.SetTouch(true);

                    //if (!lastTouch)
                    //{
                    //    lastTouch = true;
                    //    lastAngle = transform.rotation;
                    //}
                }
            }
            else
            {
                whiteboard.SetTouch(false);
                //lastTouch = false;
            }
        }
        //if (lastTouch)
        //{
        //    transform.rotation = lastAngle;
        //}
    }
}
