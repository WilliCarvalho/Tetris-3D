using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Coded by Willian Carvalho
//started at: 04/28/2020-18:07 and finished at: 04/29/2020-5:35 
//Time spent: 1h 8min
//#Improvements needed#
//- Show what is the next pieces

public class TetrominoSpawn : MonoBehaviour
{
    public GameObject[] tetrominos;
    private GameObject previousTetromino;
    private GameObject tetrominoSpawned;

    // Start is called before the first frame update
    void Start()
    {
        previousTetromino = tetrominos[Random.Range(0, tetrominos.Length)];
        SpawnTetromino();
    }


    #region Method to spawn tetrominos
    public void SpawnTetromino()
    {
        //A variable that get the tetromino piece who will spawn
        tetrominoSpawned = tetrominos[Random.Range(0, tetrominos.Length)];

        //verify if the next piece to spawn is not the same to the last piece spawned
        //if is the same, the method just start again until the next piece is different
        if (tetrominoSpawned != previousTetromino)
        {
            Instantiate(tetrominoSpawned, transform.position, transform.rotation);
            previousTetromino = tetrominoSpawned;
        }
        else
        {
            SpawnTetromino();
        }
    }
    #endregion
}
