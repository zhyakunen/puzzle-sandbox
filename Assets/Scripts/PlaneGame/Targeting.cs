using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour {

    public GameObject target;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //Vector3 dir = Vector3.RotateTowards(-transform.right, target.transform.position - transform.position, speed * Time.fixedDeltaTime, 0f);
        //Debug.DrawRay(transform.position, dir);
        Debug.DrawRay(transform.position, -transform.right);
        float angle = Vector3.SignedAngle(-transform.right, target.transform.position - transform.position, Vector3.forward);
        float deltaSpeed = speed * Time.fixedDeltaTime;
        if (angle > deltaSpeed) angle = deltaSpeed;
        if (angle < -deltaSpeed) angle = -deltaSpeed;
        transform.Rotate(Vector3.forward * angle);

    }

    public void SetTarget(GameObject setTarget) {
        target = setTarget;
    }

}
