using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDrop : MonoBehaviour {

    public GameObject samplePuzzle;
    public PuzzleContainer container;
    [Range(0, 10)] public float dropRate = 2f;
    [Range(1, 10)] public int dropWidth = 1;

    public float dropWait;

	// Use this for initialization
	void Start () {
        dropWait = dropRate;
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
        int c = Random.Range(0, dropWidth);
        PuzzleTile p = Instantiate(samplePuzzle, transform.position, Quaternion.identity, container.transform).GetComponent<PuzzleTile>();
        p.SetPuzzleType((PuzzleTile.PType)Random.Range(0, 6));
        container.DropPuzzle(c, p);
    }
}
