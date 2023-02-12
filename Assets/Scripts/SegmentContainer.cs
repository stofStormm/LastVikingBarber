using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentContainer : MonoBehaviour {

    public GameObject[] segments;
    public GameObject[] segmentFabs;
    float width = 6*2;
    float destroyX = 0;
    int count = 5;

    void Start () {
        segments = new GameObject[count];
        destroyX = width * -2.5f;
        InitiateSegments();
            }
	

	void Update () {
        TrackPieces();
	}

    void TrackPieces()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            if (segments[i].transform.position.x < destroyX)
            {
                Destroy(segments[i]);
                int rand = Random.Range(0, segmentFabs.Length);
                GameObject instSegment = Instantiate(segmentFabs[rand], new Vector3(destroyX * -1,(7*2)/-2, 0), Quaternion.identity, transform) as GameObject;
                segments[i] = instSegment;
            }
        }
    }

    void InitiateSegments()
    {
        for(int i =0;i<count;i++)
        {
            int rand = Random.Range(0, segmentFabs.Length);
            GameObject instSegment = Instantiate(segmentFabs[rand], new Vector3(width*i, (7*2)/-2, 0), Quaternion.identity, transform) as GameObject;
            segments[i] = instSegment;
        }
    }
}
