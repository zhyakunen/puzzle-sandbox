using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour {


    public GameObject genObject;
    [Range(0,20)]public float width, height;
    [Range(0, 10)] public float intvFrom, intvTo;
    [Range(0, 10)] public float sizeFrom, sizeTo;

    private System.Random random;
    private float wait;

	// Use this for initialization
	void Start () {
        random = new System.Random(System.DateTime.Now.GetHashCode());
        wait = CalcWait();
    }

    // Update is called once per frame
    void FixedUpdate () {
        GenObject();
	}

    void GenObject() {
        wait -= Time.fixedDeltaTime;
        if (wait <= 0f)
        {
            //Debug.Log("beep");
            Slime s = Instantiate(genObject,GenVector(), Quaternion.identity).GetComponent<Slime>();
            s.Size = Mathf.Lerp(sizeFrom, sizeTo, (float)random.NextDouble());
            wait += CalcWait();
        }
              
    }

    Vector3 GenVector() {
        float x = transform.position.x + Mathf.Lerp(-width, width, (float)random.NextDouble());
        float y = transform.position.y + Mathf.Lerp(-height, height, (float)random.NextDouble());
        return new Vector3(x, y, transform.position.z);
    }

    private float CalcWait() {
        return Mathf.Lerp(intvFrom,intvTo,(float)random.NextDouble());
    }
}
