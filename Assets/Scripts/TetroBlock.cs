using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetroBlock : MonoBehaviour
{
    private float prevTime;
    public float fallTime;

    // for smooth user controls
    public float verticalSpeed = 0.05f;
    public float horizontalSpeed = 0.1f;

    private float verticalTimer = 0;
    public float horizontalTimer = 0;

    public AudioClip blockSound;
    private AudioSource audioSource;

    public Vector3 rotPoint;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fallTime = GameObject.Find("GameScript").GetComponent<Game>().fallTime;
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void PlayBlockSound()
    {
        audioSource.PlayOneShot(blockSound);
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
            PlayBlockSound();
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
            PlayBlockSound();
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotPoint), new Vector3(0, 0, 1), -90);
        }

        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) || Time.time - prevTime >= fallTime)
        {
            // for delaying movement of blocks
            if (horizontalTimer < horizontalSpeed)
            {
                horizontalTimer += Time.deltaTime; 
                return;
            }

            if (verticalTimer < verticalSpeed)
            {
                verticalTimer += Time.deltaTime; 
                return;
            }
            verticalTimer = 0;

            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                FindObjectOfType<Game>().DeleteRow();

                if (FindObjectOfType<Game>().IsAboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }

                this.enabled = false;
                tag = "Untagged";
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            else
            {
                FindObjectOfType<Game>().UpdateGrid(this);
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    PlayBlockSound();
                }
                
            }
            prevTime = Time.time;
        }
        
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject tempNextBlock = GameObject.FindGameObjectWithTag("activeBlock");
            FindObjectOfType<SpawnTetromino>().SaveBlock(tempNextBlock.transform);
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
