using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonObjectText : MonoBehaviour {

    Text text;

    public void UpdateText(string s) {
        text.text = s;             
    }

    void Awake()
    {
        text = GetComponent<Text>();    
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
