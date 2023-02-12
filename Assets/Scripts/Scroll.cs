using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    public float speed = 0f;
    Vector3 currentPos;

	void Start () {
		
	}

	void Update () {
        ScrollHorizontal();
	}

    void ScrollHorizontal()
    {
        currentPos = transform.position;
        transform.position = new Vector3(currentPos.x-speed*Time.deltaTime, currentPos.y, currentPos.z);
    }
}
