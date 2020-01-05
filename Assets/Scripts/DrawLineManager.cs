//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Valve.VR;

//public class DrawLineManager : MonoBehaviour
//{
//    public GameObject prefab;
//    public Rigidbody attachPoint;

//    public SteamVR_Action_Boolean spawn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

//    SteamVR_Behaviour_Pose trackedObj;
//    FixedJoint joint;

//    private void Awake()
//    {
//        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
//    }

//    List<Vector3> linePoints = new List<Vector3>();
//    LineRenderer lineRenderer;
//    public float startWidth = 1.0f;
//    public float endWidth = 1.0f;
//    public float threshold = 0.001f;
//    Camera thisCamera;
//    int lineCount = 0;

//    Vector3 lastPos = Vector3.one * float.MaxValue;


//    void Awake()
//    {
//        thisCamera = Camera.main;
//        lineRenderer = GetComponent<LineRenderer>();
//    }

//    void Update()
//    {
//        Debug.Log("Update");
//        if (Input.GetMouseButtonDown(0))
//        {
//            Vector3 mousePos = Input.mousePosition;
//            mousePos.z = thisCamera.nearClipPlane;
//            Debug.Log(mousePos);
//            Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);

//            float dist = Vector3.Distance(lastPos, mouseWorld);
//            if (dist <= threshold)
//                return;

//            lastPos = mouseWorld;
//            if (linePoints == null)
//                linePoints = new List<Vector3>();
//            linePoints.Add(mouseWorld);

//            UpdateLine();
//        }
//    }


//    void UpdateLine()
//    {
//        Debug.Log("UpdateLine");
//        lineRenderer.startWidth = startWidth;
//        lineRenderer.endWidth = endWidth;
//        lineRenderer.positionCount = linePoints.Count;

//        for (int i = lineCount; i < linePoints.Count; i++)
//        {
//            lineRenderer.SetPosition(i, linePoints[i]);
//        }
//        lineCount = linePoints.Count;
//    }
//}