using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePusherAdder : MonoBehaviour {

    public PuzzlePusher samplePusher;
    public PuzzleContainer container;
    public CommonRandomTable randomPuzzleTypeTable;
    public PuzzleContainer.Dir dir;
    public Collider collider;

    [Range(1, 10)] public int randomStack = 3;

    public void OnClick(CommonTouchContral.TouchCursor t)
    {
        PuzzlePusher p = Instantiate(samplePusher, container.transform);
        p.SetPuzzleType((PuzzleTile.PType)randomPuzzleTypeTable.Get());
        container.AddPusher(dir);
    }

    public void SetDirection(PuzzleContainer.Dir d)
    {
        dir = d;
    }

    public void SetContainer(PuzzleContainer c)
    {
        container = c;
    }

    void Awake()
    {
        randomPuzzleTypeTable = new CommonRandomTable(randomStack,4);
        collider = GetComponentInChildren<Collider>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
