using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationWithSpeed : MonoBehaviour {

    [Range(0, 1000)] public float speed;

    Vector3 directtion;

    // Use this for initialization
    void Start()
    {
        directtion = Random.onUnitSphere;
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(speed * Time.fixedDeltaTime * directtion);
    }
}
