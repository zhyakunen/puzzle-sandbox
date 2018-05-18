using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonChangeColor : MonoBehaviour, ICommonColorable
{
    public CommonColorContral colorContral;
    public Color color;
    public string colorName;

    Material mat;


    void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        
    }

    // Use this for initialization
    void Start()
    {
        if (colorContral != null) colorContral.AddToColorList(this);
        mat.SetColor(colorName, color);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetColor(Color c)
    {
        color = c;
        mat.SetColor(colorName, color);
    }

}
