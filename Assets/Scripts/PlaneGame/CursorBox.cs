using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBox : MonoBehaviour {

    float life;

	// Use this for initialization
	void Start () {
        life = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.Rotate(Vector3.back,Time.fixedDeltaTime*720f);
        life -= Time.fixedDeltaTime;
        if (life < 0f) {
            Destroy(gameObject);
        }
    }
}
