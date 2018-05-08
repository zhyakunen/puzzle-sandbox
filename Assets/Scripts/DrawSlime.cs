using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSlime : MonoBehaviour {

    static float killY = 300f * 300f;

    Animator animator;

    float bangTime;
    float size;
    float damage;
    int touch;
    public float Size
    {
        get { return size; }
        set
        {
            size = value;
            transform.localScale = new Vector3(size, size, size);
            gameObject.GetComponent<Rigidbody2D>().mass = Mathf.Pow(size, 3);
            //Debug.Log(size);
        }
    }
    public GameObject bangObject;
    void Awake()
    {
        bangTime = 5f;
        Size = 1f;
        touch = -1;
        damage = 0f;
    }

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.sqrMagnitude >= killY) Destroy(gameObject);

        bangTime -= Time.fixedDeltaTime;
        if (bangTime <= 0f) Bang();
    }

    void Bang()
    {
        SlimeBang sb = Instantiate(bangObject, transform.position + new Vector3(0f, size / 2f, 0f), Quaternion.identity).GetComponent<SlimeBang>();
        sb.bang(size);
        Destroy(gameObject);
    }


}
