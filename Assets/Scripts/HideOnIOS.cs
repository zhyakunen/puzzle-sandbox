using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnIOS : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
