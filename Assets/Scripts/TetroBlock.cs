using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetroBlock : MonoBehaviour
{

    private float prevTime;
    public float fallTime = 0.8f;

    public static int height = 20;
    public static int width = 10;

    public Vector3 rotPoint;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotPoint), new Vector3(0, 0, 1), -90);
        }

        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) || Time.time - prevTime >= fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                DeleteRow();

                if (FindObjectOfType<Game>().IsAboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            prevTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundY < height)
                grid[roundX, roundY] = children;
        }
    }

    // for clearing rows

    bool IsFullRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    void RowDown(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y-1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    void MoveAllRows(int y)
    {
        for (int i = y; i < height; ++i)
        {
            RowDown(i);
        }
    }

    void DeleteRow()
    {
        for (int y = 0; y < height; ++y)
        {
            if (IsFullRow(y))
            {
                DeleteLine(y);
                MoveAllRows(y+1);
                y--;
            }
        }
    }

    bool ValidMove()
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
                return false;
        }
        return true;
    }
}
