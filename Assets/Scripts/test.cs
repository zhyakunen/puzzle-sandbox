using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string a = "oaghasioaasdf";
        char[] ca = a.ToUpper().ToCharArray();
        Array.Sort(ca);

        Debug.Log(new string(ca));
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
