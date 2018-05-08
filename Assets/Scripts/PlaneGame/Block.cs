using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour,ISpawnable {

    public GameObject movement;
    IMovement _movement;

    private void Awake()
    {
        _movement = movement.GetComponent<IMovement>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.position += _movement.Move();    
    }

    public void SetTarget(GameObject go) {

    }
}
