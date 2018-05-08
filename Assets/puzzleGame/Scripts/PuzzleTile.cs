using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour {

    public enum PType { R,G,B,C,M,Y};
    public PType puzzleType = PType.R;

    ColorControllor colorControllor;
    
    public void SetColor(Color c) {
        colorControllor.SetColor(c);
    }

    public void SetPuzzleType(PType t) {
        puzzleType = t;
        switch (t) {
            case PType.R:
                colorControllor.SetColor(Color.red);
                break;
            case PType.G:
                colorControllor.SetColor(Color.green);
                break;
            case PType.B:
                colorControllor.SetColor(Color.blue);
                break;
            case PType.C:
                colorControllor.SetColor(Color.cyan);
                break;
            case PType.M:
                colorControllor.SetColor(Color.magenta);
                break;
            case PType.Y:
                colorControllor.SetColor(Color.yellow);
                break;
        }
    }

    public void Move(Vector3 pos)
    {
        transform.position = pos;
    }

    void Awake()
    {
        colorControllor = GetComponent<ColorControllor>();    
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    

}
