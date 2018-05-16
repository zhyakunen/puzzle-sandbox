using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour {

    public enum PType { R,G,B,C,M,Y};
    public PType puzzleType = PType.R;
    public bool moving;
    public int col;
    public float speed = 10f;
    public PuzzleContainer container;

    ColorControllor colorControllor;
    Vector3 moveTo;
    
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

    public void SetContainer(PuzzleContainer c)
    {
        container = c;
    }

    public void Kill() {
        Destroy(gameObject);
    }

    public void Move(Vector3 pos)
    {
        moveTo = pos;
        if (transform.position != moveTo) moving = true;

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

    void FixedUpdate()
    {
        MoveUpdate();   
    }

    void MoveUpdate()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.fixedDeltaTime);
            if(transform.position == moveTo)
            {
                container.PuzzleEndMove();
                moving = false;
            }
        }
    }


}
