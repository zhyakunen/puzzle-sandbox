using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchContral : MonoBehaviour {

    public GameObject sampleTouchBall;
    public Camera mainCam;    
    [Range(0,2)]public float touchSize;

    float screenRatio;
    Dictionary<int, TouchBall> touchBalls;

    // Use this for initialization
    void Start () {
        touchBalls = new Dictionary<int, TouchBall>();
        screenRatio = 0f;
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        screenRatio = mainCam.orthographicSize / Screen.height;
        Physics2D.gravity = new Vector2(Input.gyro.gravity.x*9.8f, Input.gyro.gravity.y*9.8f);

        for (var i = 0; i < Input.touchCount; i++) {
            Touch t = Input.GetTouch(i);
            
            switch (t.phase) {
                case TouchPhase.Began:
                    NewTouchBall(t);
                    break;
                case TouchPhase.Moved:
                    MoveTouchBall(t);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    RemoveTouchBall(t);
                    break;
                case TouchPhase.Canceled:
                    RemoveTouchBall(t);
                    break;


            }
        }
	}

    void NewTouchBall(Touch t) {
        int index = t.fingerId;
        
        TouchBall b = Instantiate(sampleTouchBall, FindTouchPoint(t), Quaternion.identity).GetComponent<TouchBall>();
        b.Size = touchSize;
        b.phase = t.phase;
        b.fingerId = index;
        touchBalls[index] = b;
    }

    void MoveTouchBall(Touch t) {
        int index = t.fingerId;
        TouchBall b = touchBalls[index];
        b.phase = t.phase;
        b.gameObject.transform.position = FindTouchPoint(t);

    }

    void RemoveTouchBall(Touch t) {
        int index = t.fingerId;
        TouchBall b = touchBalls[index];
        b.phase = t.phase;
        touchBalls.Remove(index);
        Destroy(b.gameObject, 0.1f);
    }

    Vector3 FindTouchPoint(Touch t) {
        Vector3 rpos = new Vector3(t.position.x * 2f - Screen.width, t.position.y * 2f - Screen.height, 0f);
        Vector3 pos = mainCam.transform.position + rpos * screenRatio;
        pos.z = 0f;
        return pos;
    }
}
