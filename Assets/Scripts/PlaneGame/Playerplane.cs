using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerplane : MonoBehaviour {

    public GameObject body;     
    public Vector3 moveOffset;  
    public GameObject gun;      
    public PlayerBullet bullet;
    [Range(1,1000)]public float fireRate; //Per Mins
    public ScoreObject score;
    public PlaneBang bang;

    Vector3 rotation,moveto;
    int touchid;
    float speed;
    float gunCoolTime;
    float gunHeat;
    bool dead;
    float deadTime;
    

	// Use this for initialization
	void Start () {
        rotation = new Vector3(0f, 360f, 0f);
        touchid = -1;
        speed = 5f;
        gunCoolTime = 60f / fireRate;
        gunHeat = 0f;
        dead = false;
        deadTime = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        if (dead) {
            deadTime -= Time.deltaTime;
            if (deadTime <= 0f) SceneManager.LoadScene(3);
            return;

        }
        body.transform.Rotate(rotation * Time.deltaTime);
	}

    void FixedUpdate()
    {
        if (dead) return;
        if (gunHeat > 0f) {
            gunHeat -= Time.fixedDeltaTime;
        }
        if (touchid != -1)
        {
            //Debug.Log(Vector3.MoveTowards(gameObject.transform.position, moveto, speed * Time.fixedDeltaTime));
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, moveto, speed * Time.fixedDeltaTime);
            if (gunHeat <= 0f)
            {
                PlayerBullet b = Instantiate<PlayerBullet>(bullet, gun.transform.position, Quaternion.identity);
                b.SetOwner(gameObject);
                gunHeat += gunCoolTime;
            }
        } else if(gunHeat < 0f) {
            gunHeat = 0f;
        }
    }

    public void Move(Vector3 pos,int index)
    {
        if (dead) return;
        if (touchid == -1) touchid = index;
        if (touchid == index) moveto = pos + moveOffset;            
    }

    public void EndMove(int index) {
        if (touchid == index) touchid = -1;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy"
            ||collision.gameObject.tag == "EnemyBullet"
            ||collision.gameObject.tag == "Object") {
            Kill();
        }   
    }

    public void AddScore(int s) {
        Debug.Log(s);
        score.score += s;
    }

    void Kill() {
        dead = true;
        GetComponent<Rigidbody>().detectCollisions = false;
        body.SetActive(false);
        Instantiate(bang, transform.position, Quaternion.identity);
        //Destroy(gameObject);
    }

}
