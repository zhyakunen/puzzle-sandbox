using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleContainer : MonoBehaviour {

    [Range(1, 20)] public int width = 5, height = 10;
    public float groundHeight = 0.5f;

    List<List<PuzzleTile>> puzzleMap;
    bool puzzleDirty = false;
    Vector3 puzzleOffset;

    public enum Dir {left,right};  

    //interfaces

    public void DropPuzzle(int col,PuzzleTile p) {
        puzzleMap[col].Add(p);
        puzzleDirty = true;
    }

    public void RemovePuzzle(int col, int row) {
        List<PuzzleTile> l = puzzleMap[col];
        if (l.Count > row)
        {
            PuzzleTile p = l[row];
            l.RemoveAt(row);
            puzzleDirty = true;
        }
    }

    public void InsertPuzzle(int col, PuzzleTile p) {
        puzzleMap[col].Insert(0, p);
        puzzleDirty = true;
    }

    public void PushPuzzle(int col, int row, Dir dir) {
        puzzleDirty = true;
    }

    //private method

    void Awake()
    {
        puzzleOffset = new Vector3(-width/2f,-height/2f,0f);
        puzzleMap = new List<List<PuzzleTile>>();
        for (int i=0; i < width; i++) { 
            List<PuzzleTile> l = new List<PuzzleTile>();
            l.Capacity = height + 1;
            puzzleMap.Add(l);
        }
        puzzleDirty = false;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        UpdatePuzzle();    
    }

    void UpdatePuzzle() {
        if (puzzleDirty) {
            for (int c = 0; c < width; c++) {
                for (int r = 0; r < puzzleMap[c].Count; r++) {
                    Vector3 pos = new Vector3(c, r, 0f) + puzzleOffset;
                    puzzleMap[c][r].Move(pos);
                }
            }
            puzzleDirty = false;
        }
    }
}
