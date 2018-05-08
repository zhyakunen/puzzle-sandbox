using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBang : MonoBehaviour {

    static float forceMulti = 0.5f;
    static float slimeMulti = 2f;
    static float size = 0.2f;
    public GameObject bangObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void bang(float s) {
        float f = s * s * forceMulti;
        //Handheld.Vibrate();
        for (int i = 0; i <= s*s*slimeMulti; i++) {
            Vector3 offset = Random.insideUnitSphere*size*s;
            Rigidbody2D r2d = Instantiate(bangObject, transform.position + offset, Quaternion.identity).GetComponent<Rigidbody2D>();
            r2d.AddExplosionForce(f, transform.position, f);

        }
        Destroy(gameObject);
    }
}
