using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    #region Pub Vars
    public float lifeTime = 2;
    public int damage = 1;
    #endregion

    #region Priv Vars
    float aliveTime = 0;
    #endregion

    public virtual void Start () {
		
	}
	
	public virtual void Update () {
        CheckLifeTime();
	}

    void CheckLifeTime()
    {
        aliveTime += Time.deltaTime;
        if(aliveTime>=lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
