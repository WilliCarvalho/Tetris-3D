using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Created by Willian Carvalho
//started at: 04/27/2020-20:32 and finished at: 04/29/2020-5:35 
//Time spent: 18h 17min
//#Improvements needed#
//- Make the piece movement smooth
//- bug Fix - sound effect when the game over ocurrs
//- move some methods to GameController script
//- improve movement when near the walls


public class Tetromino : MonoBehaviour
{
    //Variables who create the matrice
    public static int height = 25;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];

    private float pastTime;
    public float fallSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        #region Pieces Movement
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (ValidPosition() == false)
            {
                transform.Rotate(0, 0, 90);
            }
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (ValidPosition() == false)
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (ValidPosition() == false)
                transform.position -= new Vector3(-1, 0, 0);
        }

        if (Time.time - pastTime > (Input.GetKey(KeyCode.DownArrow) ? fallSpeed / 10 : fallSpeed))
        {
            transform.position += new Vector3(0, -1, 0);
            if (ValidPosition() == false)
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                FindObjectOfType<TetrominoSpawn>().SpawnTetromino();
                this.enabled = false;
            }
            pastTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            while(ValidPosition() == true)
            {
                transform.position += new Vector3(0, -1, 0);
            }

            if (ValidPosition() == false)
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                FindObjectOfType<TetrominoSpawn>().SpawnTetromino();
                this.enabled = false;
            }
        }       
        #endregion
    }

    #region Add piece data to the matrice method
    //Get the position of the piece that landed in a valid position and set it data to the matrice  
    public void AddToGrid()
    {
        FindObjectOfType<GameController>().PlaySoundEffect(0);
        foreach (Transform children in transform)
        {
            //Take position of each piece in the children and round it to a integer number to avoid position bugs
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }
    #endregion

    #region Methods to check valid positions in the matrice to the pieces land and if a line is full
    //A boolean method which verifies if the piece position in the matrice is is valid
    public bool ValidPosition()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    //Verify the columns of a specific line inside the matrice
    public void CheckForLines()
    {
        for (int line = height - 1; line >= 0; line--)
        {
            if (IsLineFull(line))
            {
                DeleteLine(line);
                RowDown(line);
                FindObjectOfType<GameController>().AddPoints(100);
            }
        }
    }

    //Verify if the line is full and returns a boolean value
    private bool IsLineFull(int line)
    {
        for (int column = 0; column < width; column++)
        {
            if (grid[column, line] == null)
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Methods to delete an especific line and bring the others down
    //Delete the values of an especific line of the matrice
    private void DeleteLine(int line)
    {
        FindObjectOfType<GameController>().PlaySoundEffect(1);
        for (int column = 0; column < width; column++)
        {
            Destroy(grid[column, line].gameObject);
            grid[column, line] = null;
        }
    }

    //Take the pieces that still remaining and push then down 
    //according to where the line has been deleted
    private void RowDown(int deletedLine)
    {
        for (int line = deletedLine; line < height; line++)
        {
            for (int column = 0; column < width; column++)
            {
                if (grid[column, line] != null)
                {
                    grid[column, line - 1] = grid[column, line];
                    grid[column, line] = null;
                    grid[column, line - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    #endregion

    #region GameOver mechanic
    private bool canCountGameOver;
    private void OnTriggerEnter(Collider other)
    {
        canCountGameOver = true;
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        yield return new WaitForSeconds(1.7f);
        if (canCountGameOver)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canCountGameOver = false;
    }
    #endregion
}
