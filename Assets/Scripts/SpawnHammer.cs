using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHammer : MonoBehaviour {
    public GameObject hammer;
    public Transform[] pointsT;
    public float rate = 20;
    public int chance = 30;
    public AudioClip hammerTime;

    Vector2[] points;
    float timer = 0;
    AudioSource au;

	void Start () {
        au = GetComponent<AudioSource>();
        points = new Vector2[pointsT.Length];
        FillPoints();
	}
	
	void Update () {
        timer += Time.deltaTime;
		if(timer>=rate)
        {
            SpawnCheck();
        }
	}

    void FillPoints()
    {
        for(int i=0;i<points.Length;i++)
        {
            points[i] = new Vector2(pointsT[i].position.x, pointsT[i].position.y + 1.25f);
        }
    }

    void SpawnCheck()
    {
        int rand1 = Random.Range(0, 100);
        int rand2 = Random.Range(0, points.Length);
        if(rand1<chance)
        {
            Instantiate(hammer, points[rand2], Quaternion.identity);
            au.PlayOneShot(hammerTime, 0.7f);
        }
        timer = 0;
    }
}
