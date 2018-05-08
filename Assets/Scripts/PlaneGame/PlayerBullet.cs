using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    [Range(0,100)]public float speed;
    [Range(0,1000)]public float maxDistance;
    public GameObject body;
    public float damage;

    Rigidbody rigidbody;
    HitData hit;

    void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        hit = new HitData();
        hit.damage = damage;

    }

    // Use this for initialization
    void Start () {
        ApplySpeed();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        CheckDistance();
        Resize(Time.fixedDeltaTime);
    }

    void Resize(float t)
    {
        if (t * speed > 0.2f)
        {
            gameObject.transform.localScale = new Vector3(t * speed,1f,1f);
        }
        else {
            gameObject.transform.localScale = new Vector3(0.2f,1f,1f);
        }
    }

    public void SetOwner(GameObject owner) {
        hit.owner = owner;
    }

    public void SetRotation(Quaternion q) {
        gameObject.transform.rotation = q;
        ApplySpeed();
    }

    void ApplySpeed() {
        rigidbody.velocity = gameObject.transform.right * speed;
        //Debug.Log(rigidbody.velocity);
    }

    void CheckDistance() {
        if (Mathf.Abs(transform.position.x) > maxDistance
            || Mathf.Abs(transform.position.y) > maxDistance
            || Mathf.Abs(transform.position.z) > maxDistance)
            Remove();
    }

    void Remove()
    {
        Destroy(gameObject);
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("Hit", hit);
            Kill();
        }
        else if (collision.gameObject.tag == "Object") {
            Kill();
        }   
    }
}
