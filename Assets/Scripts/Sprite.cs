using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour {

    #region Pub Vars
    public float speed;
    public float jumpForce;
    public bool facingRight = true;
    public int maxHealth = 3;
    public GameObject[] bloodSpatter;
    public GameObject dieEffect;
    #endregion

    #region Priv Vars
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    int currentHealth = 0;
    bool flashing = false;
    float flashTimer = 0;
    #endregion

    public virtual void Start () {
        AttachDependancies();
        currentHealth = maxHealth;
	}
	

	public virtual void Update () {
        if (flashing)
        {
            FlashTracker();
        }
        if (currentHealth<=0)
        {
            Die();
        }
	}

    public virtual void FixedUpdate()
    {
    
    }

    public void Move(float xVal)
    {
        rb.velocity = new Vector2(xVal*speed*Time.deltaTime, rb.velocity.y);
    }
    public void Move(float xVal,float yVal)
    {
        rb.velocity = new Vector2(xVal * speed * Time.deltaTime, yVal * speed * Time.deltaTime);
    }
    public virtual void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }

    public virtual void Flip()
    {
        
        if((rb.velocity.x>0 && !facingRight)|| (rb.velocity.x < 0 && facingRight))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = !facingRight;
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth >= damage)
        {
            currentHealth -= damage;
        }
        else
        {
            currentHealth = 0;
        }

        Flash();
    }

    public void Die()
    {
        BloodSpatter();
        Destroy(gameObject);
        Instantiate(dieEffect, transform.position, Quaternion.identity);
            }

    void BloodSpatter()
    {
        if(bloodSpatter.Length>0)
        {
            for(int i =0; i<20;i++)
            {
                int rand = Random.Range(0, bloodSpatter.Length);
                Instantiate(bloodSpatter[rand], transform.position, Quaternion.identity);
            }
        }
    }

    void AttachDependancies()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public virtual void Flash()
    {
        sr.color = Color.red;
        flashing = true;
    }

    void FlashTracker()
    {
        flashTimer += Time.deltaTime;
        if(flashTimer>0.1f)
        {
            flashing = false;
            flashTimer = 0;
            sr.color = Color.white;
        }
    }

}
