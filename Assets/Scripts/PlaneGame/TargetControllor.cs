using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControllor : MonoBehaviour {

    public Targeting[] targetings;
    public GameObject target;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTarget(GameObject setTarget)
    {
        target = setTarget;
        foreach (Targeting tar in targetings) {
            tar.SetTarget(target);
        }
    }


}
