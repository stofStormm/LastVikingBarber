using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ME1 : Sprite {

    #region Pub Vars
    float aggroDistance = 10f;
    public Transform jumpCheck;
    public Transform groundCheck;
    public LayerMask groundMask;
    public int type = 0; // 0 = Monk ; 1 = Louse ; 2 = Germ
    public int damage = 10;

    #endregion

    #region Priv Vars
    float dir = 0;
    GameObject player;
    bool grounded=true;
    float aboveDir = 0;
    //bool heighChange = false;
    bool newSPawn = true;
    bool fell = false;
    #endregion

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ChooseAboveDir();
        FindDir();
        //spawnY = transform.position.y;
        base.Start();
    }

    public override void Update()
    {
        GroundCheck();
        if (type == 1)
        {
            DirCheck();
        }
        JumpCheck();
        base.Update();
    }

    public override void FixedUpdate()
    {
        Motion();
        Flip();
        base.FixedUpdate();
    }

    void FindDir()
    {
        if(transform.position.x<0.5f)
        {
            
            dir = 1;
        }
        else
        {
            
            dir = -1;
        }
   }

    void Motion()
    {
        //FindDir();
        Move(dir);
    }

    void JumpCheck()
    {
        if(Physics2D.OverlapCircle(jumpCheck.position,0.2f,groundMask) && grounded)
        {
            Jump();
        }
    }

    void GroundCheck()
    {
         grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
    }

    void ChooseAboveDir()
    {
        int rand = Random.Range(0, 2);
        if(rand==0)
        {
            aboveDir = -1;
        }
        else
            {
            aboveDir = 1;
        }
    }

    void DirCheck()
    {
        if(!grounded)
        {
            FindDir();
        }
        if(Mathf.Abs(transform.position.x)<0.5f)
        {
            dir = aboveDir;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="BS")
        {
            other.GetComponent<BS>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.tag=="Turn")
        {
            if (type ==0)
            {
                if (!newSPawn)
                {
                    dir *= -1;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Turn")
        {
            if(newSPawn)
            {
                newSPawn = false;
            }
        }
    }

}
