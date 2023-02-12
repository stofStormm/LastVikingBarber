using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour {

    public GameObject[] levelPieces;
    public GameObject instanceLevel;
    float destroyX=-9.5f*2*2;
    float width = 0;

	void Start () {
        width = instanceLevel.transform.localScale.x * 19;
	}

	void Update () {
        TrackPieces();
	}

    void TrackPieces()
    {
        for(int i =0;i<levelPieces.Length;i++)
        {
            if(levelPieces[i].transform.position.x<destroyX)
            {
                Destroy(levelPieces[i]);
                GameObject instLev = Instantiate(instanceLevel, new Vector3(destroyX * -1,0, 0), Quaternion.identity,transform) as GameObject;
                levelPieces[i] = instLev;
            }
        }
    }
}
