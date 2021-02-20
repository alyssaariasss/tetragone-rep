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
            if(!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                FindObjectOfType<Game>().DeleteRow();

                if (FindObjectOfType<Game>().IsAboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }

                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            else
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            prevTime = Time.time;
        }
    }

    bool ValidMove()
    {
        foreach (Transform block in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(block.position);

            if (FindObjectOfType<Game>().IsInsideGrid(pos) == false)
                return false;
            if (FindObjectOfType<Game>().GetTransformAtGrid(pos) != null && FindObjectOfType<Game>().GetTransformAtGrid(pos).parent != transform)
                return false;
        }
        return true;
    }
}
