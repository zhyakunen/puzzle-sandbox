using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangP : MonoBehaviour {

    public ChangeColor changeColor;
    public GameObject movement;
    IMovement _movement;


	// Use this for initialization
	void Start () {
        _movement = movement.GetComponent<IMovement>();
 	}
	
	// Update is called once per frame
	void Update () {
        changeColor.color.a -= Time.deltaTime;
        changeColor.SetColor(changeColor.color);
        transform.Translate(_movement.Move());
        if (changeColor.color.a <= 0f) Destroy(gameObject);
	}
}
