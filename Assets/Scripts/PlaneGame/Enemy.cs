using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,ISpawnable {

    public Color normalColor, flashColor;
    public ColorControllor colorControl;
    public float HP;
    public float flashTime;
    public GameObject movement;
    public GameObject target;
    public TargetControllor targetControllor;
    public PlaneBang bang;


    private float flash;
    private IMovement _movement;

    void Awake() {
    }
	// Use this for initialization
	void Start () {
        _movement = movement.GetComponent<IMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        checkFlash();
	}

    void FixedUpdate()
    {
        gameObject.transform.Translate(_movement.Move());
        

    }

    public void Hit(HitData hit)
    {
        HP -= hit.damage;
        colorControl.SetColor(flashColor);
        flash = flashTime;
        if (HP <= 0) Kill(hit.owner);
    }

    private void checkFlash() {
        if (flash > 0f) {
            flash -= Time.fixedDeltaTime;
            if (flash <= 0f) {
                colorControl.SetColor(normalColor);
            }
        }
    }

    public void Kill(GameObject killer) {
        killer.SendMessage("AddScore", 100);
        Instantiate(bang, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetTarget(GameObject go) {
        target = go;
        targetControllor.SetTarget(go);
    }
}
