using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : MonoBehaviour {

    public AudioClip dieClip;
    float timer = 0;

    AudioSource au;
	void Start () {
        au = GetComponent<AudioSource>();
        au.PlayOneShot(dieClip);
	}
	
	void Update () {
		
	}

    void Timer()
    {
        timer += Time.deltaTime;
        if(timer>0.5f)
        {
            Destroy(gameObject);
        }
    }
}
