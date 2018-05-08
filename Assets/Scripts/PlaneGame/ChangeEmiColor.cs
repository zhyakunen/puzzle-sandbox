using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmiColor : MonoBehaviour,IColorable {

    public Color color;

    Material mat;


    void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;   
    }

    // Use this for initialization
    void Start () {
        mat.SetColor("_EmissionColor", color);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SetColor(Color c) {
        color = c;
        mat.SetColor("_EmissionColor", color);
    }

}
