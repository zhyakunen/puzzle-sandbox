using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    static float killY = 300f*300f;

    Animator animator;

    float bangTime;
    float size;
    float damage;
    int touch;
    public float Size {
        get { return size; }
        set {
            size = value;
            transform.localScale= new Vector3(size, size, size);
            gameObject.GetComponent<Rigidbody2D>().mass = Mathf.Pow(size, 3);
            //Debug.Log(size);
        }
    }
    public GameObject bangObject;
    void Awake()
    {
        bangTime = 20f;
        Size = 1f;
        touch = -1;
        damage = 0f;
    }

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.position.sqrMagnitude >= killY) Destroy(gameObject);
        if (touch != -1) {
            Handheld.Vibrate();
            damage += Time.fixedDeltaTime * 5f;
            if (damage >= size) Bang();
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hold")) {
            animator.Play("idle");
            //Debug.Log("Fix");
        }
        //bangTime -= Time.fixedDeltaTime;
        //if (bangTime <= 0f) Bang();
	}

    void Bang() {
        SlimeBang sb = Instantiate(bangObject, transform.position + new Vector3 (0f,size/2f,0f) , Quaternion.identity).GetComponent<SlimeBang>();
        sb.bang(size);
        Destroy(gameObject);
    }

    void OnPressed(TouchBall b) {
        if (touch == -1) {
            touch = b.fingerId;
            animator.Play("hold");
        }
        //Debug.Log(touch);
    }

    void OnReleased(TouchBall b) {
        if (touch == b.fingerId) {            
            animator.Play("idle");
            damage = 0f;
            touch = -1;
        }
        //Debug.Log(touch);
        //Debug.Log(b.fingerId);
    }
    
}
