using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePusher : MonoBehaviour,ICommonTouchListener {

    public CommonColorContral colorControllor,baseColorControllor;
    public PuzzleContainer container;
    public PuzzleContainer.Dir dir;
    public PuzzleTile.PType puzzleType;
    public int row;
    public bool moving, locked;
    public Collider collider;
    public CommonTouchContral.TouchCursor cursor;    

    public void TouchChange(CommonTouchContral.TouchCursor t) {
        if (!locked && cursor == t && t.phase == CommonTouchContral.TouchCursor.Phase.Up)
        {
            Vector3 v = transform.position;
            v.y = t.FindTouchPoint(container.touchPlane).y;
            transform.position = v;
            container.AddPusherToQuery(this);
            locked = true;
            cursor.SetListener(null);
            cursor = null;
            baseColorControllor.SetColor(Color.gray);
        }
    }

    public void OnTouchDown(CommonTouchContral.TouchCursor t) {
        if (!locked&&cursor==null) {
            Debug.Log("t");
            cursor = t;
            Vector3 v = transform.position;
            v.y = t.FindTouchPoint(container.touchPlane).y;
            transform.position = v;
            cursor.SetListener(this);
        }
    }

    public void SetDirection(PuzzleContainer.Dir d)
    {
        dir = d;
    }

    public void SetContainer(PuzzleContainer c)
    {
        container = c;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Move(Vector3 pos)
    {
        if(cursor==null)transform.position = pos;
    }

    public void SetPuzzleType(PuzzleTile.PType t)
    {
        puzzleType = t;
        switch (t)
        {
            case PuzzleTile.PType.R:
                colorControllor.SetColor(Color.red);
                break;
            case PuzzleTile.PType.G:
                colorControllor.SetColor(Color.green);
                break;
            case PuzzleTile.PType.B:
                colorControllor.SetColor(Color.blue);
                break;
            case PuzzleTile.PType.C:
                colorControllor.SetColor(Color.cyan);
                break;
            case PuzzleTile.PType.M:
                colorControllor.SetColor(Color.magenta);
                break;
            case PuzzleTile.PType.Y:
                colorControllor.SetColor(Color.yellow);
                break;
        }
    }

    void Awake()
    {
        collider = GetComponentInChildren<Collider>();    
    }

    // Use this for initialization
    void Start () {
        locked = false;
        cursor = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (cursor != null)
        {
            Vector3 v = transform.position;
            v.y = cursor.FindTouchPoint(container.touchPlane).y;
            transform.position = v;
        }
    }


}
