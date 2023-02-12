using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : Sprite {

    #region Pub Vars
    public Transform groundPos;
    public LayerMask groundLayer;
    public float groundRadius = 0;
    public GameObject bullet;
    public Transform gunTip;
    public float bulletSpeed = 20;
    public float fireRate = 1;
    public int jumpOnDamage = 3;
    public float levForce = 1;
    public int maxZeal = 100;
    public float jumpVel = 10;


    //AUDIO
    public AudioClip walkClip;
    public AudioClip jumpClip;

    #endregion

    #region Priv Vars
    public bool grounded = false;
    float fireTimer = 0;
    bool jumping = false;
    bool jumpingUp = false;
    float jumpTimer = 0;
    float knockBackTimer = 0;
    bool knockBack = false;
    public int currentZeal = 0;
    HairArm ha;
    Animator anim;
    AudioSource au;
    bool jumpSound=false;

    #endregion

    public override void Start()
    {
        fireTimer = fireRate;
        currentZeal = maxZeal;
        ha = GameObject.Find("HairArm").GetComponent<HairArm>();
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        base.Start();
    }

    public override void Update()
    {
        Flip();
        GroundCheck();
        if((Input.GetButtonDown("Jump") && grounded)||jumping)
        {
            Jump();
        }
        //Levitate();
        //Fire();
        if (Input.GetAxis("Horizontal")!=0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
        PlaySound();
        base.Update();
    }

    public override void FixedUpdate()
    {
        if (!knockBack)
        {
            ScrollMove();
        }
        if (knockBack)
        {
            KnockBackTracker();
        }

        /*if(knockBack)
        {
            KnockBackTracker();
        }*/
        base.FixedUpdate();
    }

    void ScrollMove()
    {
        Move(Input.GetAxis("Horizontal"));

    }

    void GroundCheck()
    {
        bool groundTest= Physics2D.OverlapCircle(groundPos.position, groundRadius, groundLayer);
        if (!grounded && groundTest)
        {
            grounded = true;
            jumping = false;
            jumpSound = false;
            rb.gravityScale = 4;
            if (knockBack)
            {
                knockBack = false;
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
        }
        else if(!groundTest)
        {
            grounded = false;
        }
    }

    public override void  Flip()
    {
        if(Input.GetAxis("Horizontal")>0 && !facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale=scale;
            facingRight = !facingRight;
        }
        else if(Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = !facingRight;
        }
    }

    void Fire()
    {
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            float dir = 1;
            if(!facingRight)
            {
                dir = -1;
            }
            GameObject instBullet = Instantiate(bullet, gunTip.position, Quaternion.identity) as GameObject;
            instBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed*dir, 0);
            fireTimer = 0;
        }
    }

    public override void Jump()
    {
        if (!jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVel);
            au.PlayOneShot(jumpClip,0.04f);
            jumpSound = true;
            jumping = true;
            jumpingUp = true;
            jumpTimer = 0;
        }
        else if(jumpingUp)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer>0.5)
            {
                rb.gravityScale = 4;
                jumpingUp = false;
            }
        }
    }

    public void KnockBack(float deltaX)
    {
        knockBack = true;
        TakeDamage(0);
        rb.AddForce(new Vector2(jumpForce*deltaX, /*jumpForce/3*/0));
    }

    void KnockBackTracker()
    {
        knockBackTimer += Time.deltaTime;
        if (knockBackTimer > 0.5f)
        {
            knockBack = false;
            knockBackTimer = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public override void Flash()
    {
        sr.color = Color.white;
        //flashing = true;
    }

    void PlaySound()
    {
        if(grounded && rb.velocity.x!=0 && !jumpSound)
        {
            au.clip = walkClip;
        }

        if (grounded && rb.velocity.x == 0 && !jumpSound)
        {
            au.clip = null;
        }

        if (au.isPlaying == false)
        { 
            au.Play();
        }
    }

    /*  void Levitate()
      {
          if(jumping && Input.GetButton("Jump"))
          {
              if(rb.velocity.y<5)
              {
                  rb.AddForce(new Vector2(0, jumpVel));
                  currentZeal -= 1;
                  print(currentZeal);
              }
          }
      }*/

    void OnCollisionStay2D(Collision2D coll)
    {        
        GameObject other = coll.gameObject;

        if(other.tag=="Enemy")
        {
            Vector2 otherT = other.transform.position;
            Vector2 delta = transform.position - other.transform.position;
                KnockBack(delta.x);
        }
    }

 /*   void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject other = coll.gameObject;

        if (other.tag == "Enemy")
        {
            Vector2 otherT = other.transform.position;
            Vector2 delta = transform.position - other.transform.position;
            if (Mathf.Abs(delta.x) > 0.2f)
            {
                KnockBack(delta.x);
            }
        }
    }*/


}
