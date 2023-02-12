using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    #region Pub Vars
    public Image hpBar;
    public Image fBar;
    public Text timeLabel;
    public GameObject ht;
    #endregion

    #region Priv Vars
    BS bs;
    GlobalScript gs;
    HairArm ha;
    float hpBarRatio=0;
    float hpBarX;
    float hpBarWidth;
    float fBarRatio = 0;
    float fBarX;
    float fBarWidth;
    Camera cam;
    #endregion

    void Start () {
        cam = Camera.main;
        gs = GameObject.Find("GlobalController").GetComponent<GlobalScript>();
        bs = GameObject.Find("BaberShop").GetComponent < BS>();
        ha = GameObject.Find("HairArm").GetComponent<HairArm>();

        hpBarRatio = hpBar.rectTransform.sizeDelta.x / bs.maxHP;
        hpBarX = hpBar.transform.position.x;
        hpBarWidth = hpBar.rectTransform.sizeDelta.x;

        fBarRatio = fBar.rectTransform.sizeDelta.x / ha.maxFuel;
        fBarX = fBar.transform.position.x;
        fBarWidth = fBar.rectTransform.sizeDelta.x;

        timeLabel.text = "0 s";
    }
	

	void Update () {
     UpdateHealthBar();
        UpdateFuelBar();
        UpdateTimeLabel();
	}

    void UpdateHealthBar()
    {
        hpBar.rectTransform.sizeDelta = new Vector2(bs.curHP * hpBarRatio, hpBar.rectTransform.sizeDelta.y);
        hpBar.transform.position = new Vector2(hpBarX - ((hpBarWidth - hpBar.rectTransform.sizeDelta.x) / 2), hpBar.transform.position.y);
    }

    void UpdateFuelBar()
    {
        fBar.rectTransform.sizeDelta = new Vector2(ha.curFuel * fBarRatio, fBar.rectTransform.sizeDelta.y);
       fBar.transform.position = new Vector2(fBarX - ((fBarWidth - fBar.rectTransform.sizeDelta.x) / 2), fBar.transform.position.y);
    }

    void UpdateTimeLabel()
    {
        string text = ((int)gs.timeScore).ToString()+" s";
        timeLabel.text = text;
    }

    public void ActivateHT(Transform hammer)
    {
        ht.transform.position =cam.WorldToScreenPoint(new Vector3(hammer.position.x+2,hammer.position.y-0.5f,hammer.position.z));
        ht.GetComponent<HammerCounter>().timer = 0;
        ht.SetActive(true);
    }

    public void HideHT()
    {
        ht.SetActive(false);
    }

}
