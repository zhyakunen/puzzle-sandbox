using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControllor : MonoBehaviour,IColorable {

    public Color color;

    public GameObject[] objectList;
    IColorable[] _objectList;

    void Awake()
    {
        if (objectList.Length > 0)
        {
            _objectList = new IColorable[objectList.Length];
            for (int i = 0; i < objectList.Length; i++)
            {
                _objectList[i] = objectList[i].GetComponent<IColorable>();
            }
        }
    }

    // Use this for initialization
    void Start () {
        _SetColor();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetColor(Color c) {
        color = c;
        _SetColor();
    }

    void _SetColor() {
        for (int i = 0; i < _objectList.Length; i++)
        {
            _objectList[i].SetColor(color);
        }
    }
}
