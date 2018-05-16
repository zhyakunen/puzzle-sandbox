using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInserter : MonoBehaviour
{

    public ColorControllor colorControllor;
    public PuzzleContainer container;
    public Collider collider;

    CommonRandomTable randomTable;

    public void OnTouchDown(CommonTouchContral.TouchCursor t)
    {
        container.InsertPuzzleRow();
    }

    public void SetContainer(PuzzleContainer c)
    {
        container = c;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Move(Vector3 pos)
    {
        transform.position = pos;
    }


    void Awake()
    {
        collider = GetComponentInChildren<Collider>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }


}
