using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    #region Pub Vars

    #endregion

    #region Priv Vars

    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy")
        {
            other.GetComponent<Sprite>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }


}
