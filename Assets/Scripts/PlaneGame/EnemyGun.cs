using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    public int magSize,bulletPerShot;
    public float angle,reloadTime,fireRate;
    public GameObject bullet;

    int shoot;
    float cooling, reloading,coolTime;

	// Use this for initialization
	void Start () {
        coolTime = 60f / fireRate;
        reloading = reloadTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (reloading > 0f)
        {
            reloading -= Time.fixedDeltaTime;
            cooling = 0f;
            if (reloading <= 0f)
            {
                shoot = magSize;
            }
        }
        else if (shoot <= 0)
        {
            reloading = reloadTime;
        }
        else if (cooling > 0f) {
            cooling -= Time.fixedDeltaTime;
        }
        else {
            Fire();
            cooling += coolTime;
            --shoot; 
        }

    }

    void Fire() {
        if (bulletPerShot == 1)
        {
            Instantiate(bullet, gameObject.transform.position, transform.rotation);
        } else if (bulletPerShot > 1) {
            Quaternion from = Quaternion.Euler(new Vector3(0f, 0f, -(angle/2)));
            Quaternion step = Quaternion.Euler(new Vector3(0f, 0f, angle / (bulletPerShot-1)));
            //Debug.Log((transform.rotation).eulerAngles);
            for (int i = 0; i < bulletPerShot; i++)
            {
                Instantiate(bullet, transform.position, transform.rotation * from);
                from *= step;
            }
        }
    }
}
