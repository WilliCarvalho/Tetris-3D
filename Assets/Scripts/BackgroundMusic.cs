using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by Willian Carvalho
//started at: 04/30/2020-8:00 and finished at: 04/30/2020-9:00 
//Time spent: 1h
//#Improvements needed#
//- add a singleton
//- made this script a real audio manager
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    public AudioSource backGroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(backGroundMusic);
    }
}
