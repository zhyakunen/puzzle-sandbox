using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBang : MonoBehaviour {

    public float speed;
    public int count;
    public GameObject bangObject;

    // Use this for initialization
    void Start()
    {
        Bang();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Bang()
    {
        for (int i = 0; i <= count; i++)
        {
            Instantiate(bangObject, transform.position, Random.rotation).GetComponentInChildren<IMovement>().SetSpeed(speed*Random.value);
            
        }
        Destroy(gameObject);
    }

}
