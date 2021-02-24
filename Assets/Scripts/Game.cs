using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // for grid limits
    private static int height = 20;
    private static int width = 10;

    // stores x and y position of the blocks
    private static Transform[,] grid = new Transform[width, height];

    // fixed value for every line cleared
    public int scoreOneRow = 50;
    public int scoreTwoRow = 100;
    public int scoreThreeRow = 300;
    public int scoreFourRow = 1000;

    private int currentScore = 0;
    public int maxScore = 500;

    // for handling text display
    public Text canvasScore;
    public Text canvasLines;
    public Text canvasTime;

    // for timer
    public float timeLeft = 10;
    public bool isDecreasing = false;

    // for tracking lines cleared 
    private int RowsCleared = 0;
    private int currentLines = 0;

    void Start()
    {
        isDecreasing = true;
        Timer(timeLeft);
    }

    void Update()
    {
        UpdateScore();
        UpdateUI();
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            Timer(timeLeft);
        }
        else
        {
            if (currentScore < maxScore)
            {
                GameOver();
                timeLeft = 0;
                isDecreasing = false;
            }
            else if (currentScore >= maxScore)
            {
                // try lang 
                SceneManager.LoadScene("EasyMode");
            }
            
         
        }
    }

    void Timer(float displayTime)
    {
        displayTime += 1; 

        float minLeft = Mathf.FloorToInt(displayTime / 60);
        float secLeft = Mathf.FloorToInt(displayTime % 60);

        canvasTime.text = string.Format("{0:00}:{1:00}", minLeft, secLeft);
    }

    public void UpdateScore()
    {
        if (RowsCleared > 0)
        {
            if (RowsCleared == 1)
            {
                currentLines += 1;
                currentScore += scoreOneRow;
            }
            else if (RowsCleared == 2)
            {
                currentLines += 2;
                currentScore += scoreTwoRow;
            }
            else if (RowsCleared == 3)
            {
                currentLines += 3;
                currentScore += scoreThreeRow;
            }
            else if (RowsCleared == 4) // max is 4 because the longest block == 4 lines
            {
                currentLines += 4;
                currentScore += scoreFourRow;
            }
            RowsCleared = 0;
        }
    }

    // updates score displayed in game 
    public void UpdateUI()
    {
        canvasScore.text = currentScore.ToString();
        canvasLines.text = currentLines.ToString();
    }

    // checks if block is outside grid limits
    public bool IsAboveGrid(TetroBlock tetroblock)
    {
        for (int x = 0; x < width; ++x)
        {
            foreach (Transform block in tetroblock.transform)
            {
                Vector2 pos = Round(block.position);
                if (pos.y > height - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // checks if a line is full, if true add 1 to rowscleared
    public bool IsFullRow(int y)
    {
        // y represents row w/c will be used to check each x pos
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] == null)
                return false; // because the row is not full
        }
        // for every full row, increment by 1
        RowsCleared++;
        return true;
    }

    // lines 98 to 140 are needed for deleting rows
    public void DeleteLine(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // this moves only 1 row down 
    public void RowDown(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // for loop to move ALL rows above a cleared line
    public void MoveAllRows(int y)
    {
        for (int i = y; i < height; ++i)
        {
            RowDown(i);
        }
    }

    // main method called in TetroBlock to start deleting rows
    public void DeleteRow()
    {
        for (int y = 0; y < height; ++y)
        {
            if (IsFullRow(y))
            {
                DeleteLine(y);
                MoveAllRows(y + 1);
                --y;
            }
        }
    }

    // takes position of each block in main program to update the grid array 
    public void UpdateGrid(TetroBlock tetroblock)
    {
        for (int y = 0; y < height; ++y)
        {
            // check x and y to update grid position
            for (int x = 0; x < width; ++x)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetroblock.transform)
                    {
                        // set grid values to null to remove them from array
                        grid[x, y] = null;
                    }
                }
            }
        }
        // used to avoid overlapping of blocks
        foreach (Transform block in tetroblock.transform)
        {
            Vector2 pos = Round(block.position);
            if (pos.y < height)
            {
                grid[(int)pos.x, (int)pos.y] = block;
            }
        }
    }

    // gets transform position of block to limit overlapping
    public Transform GetTransformAtGrid(Vector2 pos)
    {
        if (pos.y > height - 1)
            return null;
        else
            return grid[(int)pos.x, (int)pos.y];
    }

    // validate position of block for instantiating and swapping positions
    public bool IsValidPos(GameObject tetroblock)
    {
        foreach (Transform block in tetroblock.transform)
        {
            Vector2 pos = Round(block.position);
            if (!IsInsideGrid(pos))
                return false;
            if (GetTransformAtGrid(pos) != null && GetTransformAtGrid(pos).parent != tetroblock.transform)
               return false;
        }
        return true;
    }

    // checks x and y position of the blocks
    public bool IsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    // generates new method for rounding x and y positions to avoid repeating Mathf function
    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}