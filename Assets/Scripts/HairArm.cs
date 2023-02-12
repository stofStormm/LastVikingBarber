using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairArm : MonoBehaviour {

    #region Pub Vars
    public Sprite spikeSpr;
    public Sprite posionSpr;
    public Sprite flameSpr;
    public float sloMoTime = 0.5f;
    public float sloMoScale = 0.5f;
    public int maxFuel = 100;
    public int fuelGain = 5;
    public float flameTime = 4;

    //AUDIO
    public AudioClip spikeClip;
    public AudioClip poisonClip;
    public AudioClip flameClip;
    public AudioClip burnClip;
    public AudioClip ouchClip;
    public AudioClip fuelOut;
    public AudioClip fuelFull;
    #endregion

    #region Priv Vars
    GameObject player;
    PlayerSprite ps;
    Vector2 offset;
    int state = 0; // 0 = Spike ; 1 = Poison ; 2 = Flame
    SpriteRenderer sr;
    float sloMoTimer = 0;
    public bool sloMo = false;
    Enemy_ME1 curEnemy;
    public int curFuel = 0;
    bool flameOn = false;
    float flameTimer = 0;
    int lastState = 0;
    Animator anim;
    AudioSource au;
    int oldFuel = 0;
    #endregion

    void Start () {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerSprite>();
        offset = transform.position- player.transform.position;
        sr = GetComponent<SpriteRenderer>();
        curFuel = 0;
	}	

	void Update () {
        FollowPlayer();
        SwitchState();
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            FlameOn();
        }
        if(flameOn)
        {
            FlameOff();
        }

        if(flameOn && !au.isPlaying)
        {
            au.clip=burnClip;
            au.Play();
        }



    }

    void FixedUpdate()
    {
        SloMoTracker();
    }

    void FollowPlayer()
    {
        if (ps.facingRight)
        {
            transform.position = player.transform.position + new Vector3(offset.x, offset.y, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.position = player.transform.position + new Vector3(-offset.x, offset.y, 0);
            transform.localScale = new Vector3(-1, 1,1);
        }
    }

    void SwitchState()
    {
        if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!flameOn)
            {
                state = 0;
            anim.SetBool("poison", false);
            
                au.PlayOneShot(spikeClip, 0.3f);
            }
            //Debug.Log("Spike");
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!flameOn)
            {
                state = 1;
            anim.SetBool("poison", true);
            
                au.PlayOneShot(poisonClip, 0.3f);
            }
            //Debug.Log("Poison");
        }
        /*else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            state = 2;
            Debug.Log("Flame");
        }*/
    }

    void GainFuel(int fuel)
    {
        if(curFuel+fuel<maxFuel)
        {
            curFuel += fuel;
        }
        else
        {
            if (curFuel != maxFuel)
            {
                au.PlayOneShot(fuelFull, 1f);
            }
            curFuel = maxFuel;
        }
    }
    void FlameOn()
    {
        if(curFuel<maxFuel)
        {
            //Debug.Log("No fuel");
            au.PlayOneShot(fuelOut, 0.5f);
        }
        else
        {
            lastState = state;
            state = 2;
            flameOn = true;
            anim.SetBool("flame", true);
            //Debug.Log("Flame");
            au.PlayOneShot(flameClip);
        }
    }
    void FlameOff()
    {
        flameTimer += Time.deltaTime;
        if(flameTimer>=flameTime)
        {
            state = lastState;
            if(state==1)
            {
                anim.SetBool("poison", true);
            }
            flameOn = false;
            flameTimer = 0;
            curFuel = 0;
            anim.SetBool("flame", false);
            au.clip = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy")
        {           
            EnemyColl(other.gameObject);
        }
    }

    void EnemyColl(GameObject enemy)
    {
        Enemy_ME1 es = enemy.GetComponent<Enemy_ME1>();
        if(es.type==state || state==2)
        {
            //SloMo(es);
            es.TakeDamage(100);
            GainFuel(fuelGain);
        }
        else
        {
            ps.KnockBack(transform.position.x-enemy.transform.position.x);
            au.PlayOneShot(ouchClip,0.5f);
        }
    }

    void SloMo(Enemy_ME1 es)
    {
        sloMo = true;
        Time.timeScale = sloMoScale;
        curEnemy = es;
    }

    void SloMoTracker()
    {
        if(sloMo)
        {
            sloMoTimer += Time.deltaTime;
            if(sloMoTimer>=sloMoTime)
            {
                sloMo = false;
                Time.timeScale = 1f;
                sloMoTimer = 0;
                curEnemy.TakeDamage(100);
            }
        }
    }
}
