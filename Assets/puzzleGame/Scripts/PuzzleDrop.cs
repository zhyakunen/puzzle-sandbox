using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDrop : MonoBehaviour {

    public GameObject samplePuzzle;
    public PuzzleContainer container;
    public CommonRandomTable randomPuzzleTypeTable;
    public CommonRandomTable randomPuzzleColTable;
    public int randomTableSize, randomTableIndex;
    [Range(0, 10)] public float dropRate = 2f;
    [Range(1, 10)] public int dropWidth = 1;
    [Range(1, 10)] public int randomStack = 3;

    public float dropWait;



	// Use this for initialization
	void Start () {
        dropWait = dropRate;
        randomPuzzleColTable = new CommonRandomTable(1, dropWidth);
        randomPuzzleTypeTable = new CommonRandomTable(randomStack, 4);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        dropWait -= Time.fixedDeltaTime;
        if (dropWait <= 0f) {
            DropPuzzle();
            dropWait += dropRate;
        }
    }

    void DropPuzzle() {
        int c = randomPuzzleColTable.Get();
        PuzzleTile p = Instantiate(samplePuzzle, transform.position, Quaternion.identity, container.transform).GetComponent<PuzzleTile>();
        p.col = c;
        p.SetPuzzleType((PuzzleTile.PType)randomPuzzleTypeTable.Get());
        container.DropPuzzle(c, p);
    }



    

}
