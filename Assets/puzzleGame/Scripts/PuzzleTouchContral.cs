using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTouchContral : MonoBehaviour {

    public Camera mainCam;
    public GameObject touchPlane;
    public GameObject cursor;
    public PuzzleContainer container;

    float screenRatio;
    Dictionary<int, TouchCursor> touchCursors;

    class TouchCursor
    {

        public Ray ray;
        public TouchPhase phase;
        public int fingerId;

        public TouchCursor(Touch t, Ray r)
        {
            ray = r;
            phase = t.phase;
            fingerId = t.fingerId;
        }

        public void Move(Ray r)
        {
            ray = r;
        }

        public Vector3 FindTouchPoint(Collider c)
        {
            RaycastHit hit;
            if (c.Raycast(ray, out hit, Mathf.Infinity))
            {
                return hit.point;

            }
            return Vector3.zero;
        }
    }

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        touchCursors = new Dictionary<int, TouchCursor>();
        //Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //screenRatio = mainCam.orthographicSize / Screen.height;

        for (var i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    NewTouch(t);
                    break;
                case TouchPhase.Moved:
                    MoveTouch(t);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    RemoveTouch(t);
                    break;
                case TouchPhase.Canceled:
                    RemoveTouch(t);
                    break;


            }
        }
    }

    void NewTouch(Touch t)
    {
        int index = t.fingerId;

        TouchCursor c = new TouchCursor(t, FindTouchRay(t));
        touchCursors[index] = c;


        //Instantiate(cursor, FindTouchPoint(t), Quaternion.identity);
    }

    void MoveTouch(Touch t)
    {
        if (touchCursors.ContainsKey(t.fingerId))
        {
            TouchCursor c = touchCursors[t.fingerId];
            c.phase = t.phase;
            c.ray = FindTouchRay(t);
        }
    }

    void RemoveTouch(Touch t)
    {
        if (touchCursors.ContainsKey(t.fingerId))
        {
            touchCursors.Remove(t.fingerId);
        }
    }


    Ray FindTouchRay(Touch t)
    {
        return mainCam.ScreenPointToRay(t.position);
    }

}
