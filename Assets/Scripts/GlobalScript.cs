using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour {

    public static float scrollSpeed = 0.8f;
    public float timeScore = 0;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public AudioClip buttonClip;
    public float buttonVol = 0.7f;

    Camera mainCam;
    int state = 0;
    public bool menu = false;
    AudioSource au;

	void Awake () {
        Time.timeScale = 1;
        if(menu)
        {
            state = 0;
        }
        else
        {
            state = 1;
        }
        //DontDestroyOnLoad(gameObject);
        mainCam = Camera.main;
        au = GetComponent<AudioSource>();
        //mainCam.aspect = 16 / 9;
    }
	
	void Update () {
        if (state == 1)
        {
            timeScore += Time.deltaTime;
        }

        if (!menu && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggel();
        }
		
	}

    public void GameOver()
    {
        state = 0;
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        timeScore = 0;
        au.PlayOneShot(buttonClip, buttonVol);
    }
    public void Instructions()
    {
        SceneManager.LoadScene(2);
        au.PlayOneShot(buttonClip, buttonVol);
    }
    public void Quit()
    {
        Application.Quit();
        au.PlayOneShot(buttonClip, buttonVol);
    }

    public void PauseToggel()
    {
            if(state==1)
            {
                state = 0;
                Time.timeScale = 0;
                //print("pause");
                pauseMenu.SetActive(true);
            au.PlayOneShot(buttonClip, buttonVol);
            }
            else
            {
                state = 1;
                Time.timeScale = 1;
                print("un-pause");
                pauseMenu.SetActive(false);
            au.PlayOneShot(buttonClip, buttonVol);
        }
            
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        au.PlayOneShot(buttonClip, buttonVol);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        au.PlayOneShot(buttonClip, buttonVol);
    }
}
