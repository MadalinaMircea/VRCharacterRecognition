using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WhiteboardMarker : MonoBehaviour
{
    public Whiteboard whiteboard;

    private RaycastHit touch;

    private bool lastTouch;
    private Quaternion lastAngle;

    private void Update()
    {
        //Transform blackTip = transform.Find("BlackPart");
        //float blackHeight = blackTip.transform.localScale.y;
        //Vector3 blackPos = blackTip.transform.position;

        if (Physics.Raycast(transform.position, -transform.up, out touch, transform.localScale.y))
        {
            if(touch.collider.CompareTag("Whiteboard"))
            {
                whiteboard = touch.collider.GetComponent<Whiteboard>();
                whiteboard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
                whiteboard.SetTouch(true);

                if(!lastTouch)
                {
                    lastTouch = true;
                    lastAngle = transform.rotation;
                }
            }
        }
        else
        {
            whiteboard.SetTouch(false);
            lastTouch = false;
        }
        if (lastTouch)
        {
            transform.rotation = lastAngle;
        }
    }
}
