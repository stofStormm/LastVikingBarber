using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS : MonoBehaviour {

    #region pub vars
    public int maxHP = 100;
    public AudioClip bell;
    #endregion

    #region priv vars
    public int curHP = 0;
    GlobalScript gs;
    CameraScript cs;
    AudioSource au;
    #endregion

    void Start () {
        curHP = maxHP;
        gs = GameObject.Find("GlobalController").GetComponent<GlobalScript>();
        cs = Camera.main.GetComponent<CameraScript>();
        au=GetComponent<AudioSource>();
	}

	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        cs.shake = true;
        if(curHP>damage)
        {
            curHP -= damage;
            au.PlayOneShot(bell, 0.05f);
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        gs.GameOver();
    }
    }
