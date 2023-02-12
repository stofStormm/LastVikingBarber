using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpatter : MonoBehaviour {

    public Sprite[] sprites;

    Vector2 dir;
    Transform player;
    Rigidbody2D rb;
    SpriteRenderer sr;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float dirX = transform.position.x - player.position.x;
        if (dirX > 0)
        {
            dir = new Vector2(Random.Range(0.5f, 1f), Random.Range(0f, .75f));
        }
        else
        {
            dir = new Vector2(Random.Range(-1f, -0.5f), Random.Range(0f, .75f));
        }
        rb.velocity = 15 * dir;
        float randomScale = Random.Range(0.25f, 0.5f);
        transform.localScale = new Vector2(randomScale,randomScale);

	}
	
	void Update () {
        float z = transform.rotation.eulerAngles.z;
        z += 300 * Time.deltaTime;
        transform.rotation=Quaternion.Euler(0, 0, z);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
