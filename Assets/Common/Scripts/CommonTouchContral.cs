using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonTouchContral : MonoBehaviour {

    public Camera mainCam;
    public GameObject touchPlane;
    public GameObject cursor;

    float screenRatio;
    Dictionary<int, TouchCursor> touchCursors;
    List<ICommonTouchListener> listeners;

    public class TouchCursor
    {

        public Ray ray;
        public Phase phase;
        public int fingerId,usedCount;
        public enum Phase { Down,Move,Up};
        public ICommonTouchListener listener;

        public TouchCursor(Touch t, Ray r)
        {
            ray = r;
            fingerId = t.fingerId;
            usedCount = 0;
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

        public bool Use()
        {
            bool b = (usedCount == 0);
            usedCount++;
            return b;
        }

        public void ResetUsed() {
            usedCount = 0;
        }

        public void SetListener(ICommonTouchListener l) {
            listener = l;
        }

        public void CallListener() {
            if(listener!=null)listener.TouchChange(this);
        }
    }

    public void AddListener(ICommonTouchListener l)
    {
        if (!listeners.Contains(l)) listeners.Add(l);
    }

    public void RemoveListener(ICommonTouchListener l)
    {
        if (listeners.Contains(l)) listeners.Remove(l);
    }

    void Awake()
    {
        listeners = new List<ICommonTouchListener>();
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
        c.phase = TouchCursor.Phase.Down;
        CallListener(c);
        c.CallListener();

    }

    void MoveTouch(Touch t)
    {
        if (touchCursors.ContainsKey(t.fingerId))
        {
            TouchCursor c = touchCursors[t.fingerId];
            c.phase = TouchCursor.Phase.Move;
            c.ray = FindTouchRay(t);
            CallListener(c);
            c.CallListener();
        }
    }

    void RemoveTouch(Touch t)
    {
        if (touchCursors.ContainsKey(t.fingerId))
        {
            TouchCursor c = touchCursors[t.fingerId];
            c.phase = TouchCursor.Phase.Up;
            c.ray = FindTouchRay(t);
            CallListener(c);
            c.CallListener();
            touchCursors.Remove(t.fingerId);
        }
    }


    Ray FindTouchRay(Touch t)
    {
        return mainCam.ScreenPointToRay(t.position);
    }

    void CallListener(TouchCursor t)
    {
        t.ResetUsed();
        foreach(ICommonTouchListener l in listeners)
        {
            l.TouchChange(t);
        }
    }
}
