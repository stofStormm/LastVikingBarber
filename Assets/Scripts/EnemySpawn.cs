using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    #region Pub vars
    public GameObject[] enemies;
    public float a = 1.1f;
    public float b = 3;
    public float c = 10;
    public float d = 50;
    #endregion

    #region Priv vars
    float spawnTimer = 0;
    float spawnRate = 0;
    GlobalScript gs;
    #endregion

    void Start () {
        gs = GameObject.Find("GlobalController").GetComponent<GlobalScript>();
        spawnTimer = spawnRate;
	}
	

	void Update () {
        UpdateSpawnTime();
        SpawnTimer();
	}

    void Spawn()
    {
        int rand = Random.Range(0, 3);
        Instantiate(enemies[rand], transform.position, Quaternion.identity);
    }
    void SpawnTimer()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer>=spawnRate)
        {
            int rand = Random.Range(0,100);
            if (rand > 30)
            {
                Spawn();
            }
            spawnTimer = 0;
        }
    }

    void UpdateSpawnTime()
    {
        // Eq = 1/(1.1*abs(sin(x*3))+(x/10))
        spawnRate = (1 / (a * Mathf.Abs(Mathf.Sin(gs.timeScore * b)) + (gs.timeScore / c)))*d;
    }
}
