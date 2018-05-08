using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public ScoreObject score;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        if(score == null)
            score = FindObjectOfType<ScoreObject>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "SCORE: "  + score.score ;
             
	}

    public void KillObject()
    {
        if(score != null)Destroy(score.gameObject);
    }
}
