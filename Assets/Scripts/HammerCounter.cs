using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerCounter : MonoBehaviour {

    public float time = 10;

    public float timer;
    Text text;

	void Awake () {
        timer = 0;
        text = GetComponent<Text>();
        text.text = ((double)time).ToString();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        text.text = (time-timer).ToString("F2");
    }
}
