using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour,ISpawnable {

    [Range(1,1000)]public float minSPM,maxSPM; //Spawn Per Min
    [Range(0, 50)] public float varX, varY, varZ;
    public GameObject target;


    public GameObject[] spawnObjects;
    public int[] spawnRatio;
    float spawnCooling;
    public int spawnRatioBase;

    void Awake()
    {
        spawnRatioBase = 0;
        for(int i = 0; i < spawnObjects.Length; i++)
        {
            spawnRatioBase += spawnRatio[i];
        }
    }

    // Use this for initialization
    void Start () {
        spawnCooling = 0f;
        Heat();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Spawn();    
    }

    void Heat() {
        spawnCooling += Mathf.Lerp(60f / minSPM, 60f / maxSPM, Random.value);
        Debug.Log(spawnCooling);
    }

    void Spawn() {
        if (spawnCooling > 0f) {
            spawnCooling -= Time.fixedDeltaTime;
            return;
        }
        Vector3 spawnPos = new Vector3(varX * Random.value - varX * 0.5f,
                                       varY * Random.value - varY * 0.5f,
                                       varZ * Random.value - varZ * 0.5f);
        ISpawnable spawned = Instantiate(spawnObjects[Gacha()], transform.position + spawnPos, transform.rotation).GetComponent<ISpawnable>();
        Debug.Log(spawned); 
        spawned.SetTarget(target);
        
        Heat();
    }

    int Gacha() {
        int token = Random.Range(1, spawnRatioBase+1);
        for (int i = 0; i < spawnRatio.Length; i++) {
            token -= spawnRatio[i];
            if (token <= 0) {
                Debug.Log("Gacha " + i);
                return i;
                
            } 
        }
        Debug.Log("Gacha out of range!!");
        return 0;
    }

    public void SetTarget(GameObject go) {
        target = go;
    }

}
