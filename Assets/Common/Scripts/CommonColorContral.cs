using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonColorContral : MonoBehaviour,ICommonColorable {

    public Color color;

    public GameObject[] objectList;
    public List<ICommonColorable> colorList;

    void Awake()
    {
        colorList = new List<ICommonColorable>();
        if (objectList.Length > 0)
        {
            for (int i = 0; i < objectList.Length; i++)
            {
                AddToColorList(objectList[i].GetComponent<ICommonColorable>());
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        _SetColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor(Color c)
    {
        color = c;
        _SetColor();
    }

    void _SetColor()
    {
        for (int i = 0; i < colorList.Count; i++)
        {
            colorList[i].SetColor(color);
        }
    }

    public void AddToColorList(ICommonColorable cc) {
        if (!colorList.Contains(cc)) colorList.Add(cc);
    }

}
