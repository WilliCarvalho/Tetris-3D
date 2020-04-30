using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Created by Willian Carvalho
//started at: 04/29/2020-16:32 and finished at: 04/29/2020-20:58 
//Time spent: 2h 43min
//#Improvements needed#
//- make methods more simple

public class GameController : MonoBehaviour
{
    public int points;
    public Text _score;
    public AudioClip[] soundEffects;
    private AudioSource playEffects;
    public GameObject pauseButton;
    public GameObject resumeButton;

    private void Start()
    {
        playEffects = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        _score.text = "Score: " + points.ToString();
    }


    #region GameController methods
    //Change to scene you want(used in the buttons(needs to write the name of the scene))
    public void GoToScene(string scene)
    {
        this.enabled = false;
        SceneManager.LoadScene(scene);
        if(scene == "Exit")
        {
            Application.Quit();
        }
    }

    //Add values to the 'points' integer variable 
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    //A method who's called when a sound effect wants to be played
    public void PlaySoundEffect(int effect)
    {
        playEffects.clip = soundEffects[effect];
        playEffects.Play();
    }

    public void OnPause()
    {
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        resumeButton.SetActive(true);
        FindObjectOfType<Tetromino>().enabled = false;
    }

    public void OnResume()
    {
        resumeButton.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        FindObjectOfType<Tetromino>().enabled = true;
    }
    #endregion
}
