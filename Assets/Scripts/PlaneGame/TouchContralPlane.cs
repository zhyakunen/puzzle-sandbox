using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchContralPlane : MonoBehaviour {

    //public GameObject sampleTouchBall;
    public Camera mainCam;
    [Range(0, 2)] public float touchSize;
    public GameObject touchPlane;
    public GameObject cursor;
    public Playerplane player;
    Collider planeCollider;

    float screenRatio;
    Dictionary<int, TouchCursor> touchCursors;

    class TouchCursor {

        public Vector3 pos;
        public TouchPhase phase;
        public int fingerId;

        public TouchCursor(Touch t, Vector3 ipos) {
            pos = ipos;
            phase = t.phase;
            fingerId = t.fingerId;
        }

        public void Move(Vector3 ipos) {
            pos = ipos;
        }
    }

    void Awake()
    {
        planeCollider = touchPlane.GetComponent<BoxCollider>();    

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

    void NewTouch(Touch t) {
        int index = t.fingerId;

        TouchCursor c = new TouchCursor(t, FindTouchPoint(t));
        touchCursors[index] = c;
        if(touchCursors.Count == 1) {
            player.Move(c.pos, index);
        }
        
        //Instantiate(cursor, FindTouchPoint(t), Quaternion.identity);
    }

    void MoveTouch(Touch t) {
        if (touchCursors.ContainsKey(t.fingerId))
        {
            TouchCursor c = touchCursors[t.fingerId];
            c.phase = t.phase;
            c.pos = FindTouchPoint(t);
            player.Move(c.pos, c.fingerId);
        }
    }

    void RemoveTouch(Touch t) {
        if (touchCursors.ContainsKey(t.fingerId)) {
            player.EndMove(t.fingerId);
            touchCursors.Remove(t.fingerId);
        }
    }

    Vector3 FindTouchPoint(Touch t)
    {
        Ray ray = mainCam.ScreenPointToRay(t.position);
        //Debug.Log(t.position);
        RaycastHit hit;

        if (planeCollider.Raycast(ray, out hit,Mathf.Infinity))
        {
            string selectedGameObjectname = hit.collider.gameObject.name;
            
            //Debug.Log("name=" + selectedGameObjectname);
        }
        /*
        Vector3 rpos = new Vector3(t.position.x * 2f - Screen.width, t.position.y * 2f - Screen.height, 0f);
        Vector3 pos = mainCam.transform.position + rpos * screenRatio;
        pos.z = 0f;*/
        return hit.point;
    }
}
