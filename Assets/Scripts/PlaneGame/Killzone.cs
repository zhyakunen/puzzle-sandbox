using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {

    public Vector3 kill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Vector3 p = transform.position;
        if (p.x < -kill.x || p.x > kill.x || p.y < -kill.y || p.y > kill.y)
        {
            Debug.Log("Kill");
            Destroy(gameObject);
            



        }    
    }

}
