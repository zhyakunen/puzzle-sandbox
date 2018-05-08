using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTimer : MonoBehaviour {

    public float kill;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        kill -= Time.fixedDeltaTime;
        if (kill <= 0f) Destroy(gameObject);
    }
}
