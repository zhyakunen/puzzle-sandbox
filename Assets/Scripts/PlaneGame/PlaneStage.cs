using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStage : MonoBehaviour {

    [Range(0, 10)] public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        gameObject.transform.Translate(-speed * Time.fixedDeltaTime, 0f, 0f);    
    }
}
