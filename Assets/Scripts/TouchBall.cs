using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBall : MonoBehaviour {

    public Vector3 velocity;
    public TouchPhase phase;
    public int fingerId;
    public float Size {
        set { gameObject.transform.localScale = new Vector3(value, value, value); }
    }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
            
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(phase==TouchPhase.Began) collision.attachedRigidbody.gameObject.SendMessage("OnPressed", this);    
    }

    void OnTriggerStay2D(Collider2D collision)
    { 
        if (phase == TouchPhase.Canceled || phase == TouchPhase.Ended)
            collision.attachedRigidbody.gameObject.SendMessage("OnReleased", this);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collision.attachedRigidbody.gameObject.SendMessage("OnReleased", this);
    }

}
