using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorHammer : MonoBehaviour {

    public float time = 5;
    float timer = 0;

    CameraScript cs;
    PlayerHUD hud;
    void Start () {
        cs = Camera.main.GetComponent<CameraScript>();
        hud = GameObject.Find("HUDMain").GetComponent<PlayerHUD>();
        hud.ActivateHT(transform);
    }
	

	void Update () {
        timer += Time.deltaTime;
        if(timer>=time)
        {
            hud.HideHT();
            Destroy(gameObject);
        }
	}

    void KillAll()
    {
        cs.shake = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject e in enemies)
        {
            e.GetComponent<Enemy_ME1>().TakeDamage(100);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            KillAll();
            hud.HideHT();
            Destroy(gameObject);
        }
    }
}
