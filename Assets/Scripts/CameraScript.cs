using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript: MonoBehaviour {

    public float d, v, time = 0;
    public int frameCount;

    public bool shake = false;
    float timer = 0;
    Vector3 startPos;
    int frameCounter;

	void Start () {
        startPos = transform.position;
	}
	
	void Update () {
      /*  if(Input.GetKeyDown(KeyCode.S))
        {
            shake = true;
        }*/

        if (shake)
        {
            Shake();
            ShakeTimer();
        }
	}

    void Shake()
    {
        frameCounter += 1;
        if (frameCounter >= frameCount)
        {
            d = -d;
            Vector3 curPos = transform.position;
            Vector3 newPos = new Vector3(curPos.x + d, curPos.y - d, curPos.z);
            transform.position = Vector3.Lerp(curPos, newPos, v);
            frameCounter = 0;
        }
    }

    void ShakeTimer()
    {
        timer += Time.deltaTime;
        if(timer>time)
        {
            shake = false;
            transform.position = startPos;
            timer = 0;
        }
    }
}
