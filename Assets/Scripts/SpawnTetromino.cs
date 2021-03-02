using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnTetromino : MonoBehaviour
{
    private static int height = 20;
    private static int width = 10;

    public GameObject[] Tetrominoes;

    private GameObject previewBlock;
    private GameObject nextBlock;
    private GameObject savedBlock;

    private bool gameStarted = false;

    // fixed position of next block and hold block
    private Vector2 previewBlockPos = new Vector2(15, 14);
    private Vector2 savedBlockPos = new Vector2(-7, 14);

    public int maxSwap = 3;
    private int swapCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Countdown>().StartCountdown();
    }

    public void NewTetromino()
    {
        SetNewPrevPos();
        SetNewHeight();

        if (!gameStarted)
        {
            gameStarted = true;

            nextBlock = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
            previewBlock = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewBlockPos, Quaternion.identity);
            previewBlock.GetComponent<TetroBlock>().enabled = false;
            nextBlock.tag = "activeBlock";
        }
        else
        {
            SetNewSpawnPos();
            nextBlock = previewBlock;
            nextBlock.GetComponent<TetroBlock>().enabled = true;
            nextBlock.tag = "activeBlock";

            previewBlock = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], previewBlockPos, Quaternion.identity);
            previewBlock.GetComponent<TetroBlock>().enabled = false;
        }
        swapCount = 0;
    }

    public void SaveBlock(Transform t)
    {
        swapCount++;
        SetNewSavedPos();
        SetNewHeight();

        if (swapCount > maxSwap)
            return;

        if (savedBlock != null)
        {
            GameObject tempStorage = GameObject.FindGameObjectWithTag("savedBlock");
            tempStorage.transform.localPosition = new Vector2(width / 2, height);

            if (!FindObjectOfType<Game>().IsValidPos(tempStorage))
            {
                tempStorage.transform.localPosition = savedBlockPos;
                return;
            }
            savedBlock = (GameObject)Instantiate(t.gameObject);
            savedBlock.GetComponent<TetroBlock>().enabled = false;

            savedBlock.transform.localPosition = savedBlockPos;
            savedBlock.tag = "savedBlock";

            nextBlock = (GameObject)Instantiate(tempStorage);
            nextBlock.GetComponent<TetroBlock>().enabled = true;
            nextBlock.transform.localPosition = new Vector2(width / 2, height);
            nextBlock.tag = "activeBlock";

            DestroyImmediate(t.gameObject);
            DestroyImmediate(tempStorage);
        }
        else
        {
            savedBlock = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("activeBlock"));
            savedBlock.GetComponent<TetroBlock>().enabled = false;
            savedBlock.transform.localPosition = savedBlockPos;
            savedBlock.tag = "savedBlock";

            DestroyImmediate(GameObject.FindGameObjectWithTag("activeBlock"));

            NewTetromino();
        }
    }

    public int SetNewHeight()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MediumLevel1" || scene.name == "MediumLevel2" || scene.name == "MediumLevel3")
        {
            return height = 16;
        }
        else if (scene.name == "HardLevel1" || scene.name == "HardLevel2" || scene.name == "HardLevel3")
        {
            return height = 13;
        }
        return height = 20;
    }

    public Vector2 SetNewSpawnPos()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MediumLevel1" || scene.name == "MediumLevel2" || scene.name == "MediumLevel3")
        {
            return previewBlock.transform.localPosition = new Vector2(4, 16.0f);
        }
        else if (scene.name == "HardLevel1" || scene.name == "HardLevel2" || scene.name == "HardLevel3")
        {
            return previewBlock.transform.localPosition = new Vector2(4, 13.0f);
        }
        return previewBlock.transform.localPosition = new Vector2(4, 19.0f);
    }

    public Vector2 SetNewPrevPos()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MediumLevel1" || scene.name == "MediumLevel2" || scene.name == "MediumLevel3")
        {
            return previewBlockPos = new Vector2(16.63f, 9.46f);
        }
        else if (scene.name == "HardLevel1" || scene.name == "HardLevel2" || scene.name == "HardLevel3")
        {
            return previewBlockPos = new Vector2(16.54877f, 6.231253f);
        }
        return previewBlockPos = new Vector2(16.84f, 12.5f);
    }

    public Vector2 SetNewSavedPos()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MediumLevel1" || scene.name == "MediumLevel2" || scene.name == "MediumLevel3")
        {
            return savedBlockPos = new Vector2(-8.2f, 11.8f);
        }
        else if (scene.name == "HardLevel1" || scene.name == "HardLevel2" || scene.name == "HardLevel3")
        {
            return savedBlockPos = new Vector2(-8.15f, 8.93f);
        }
        return savedBlockPos = new Vector2(-8.5f, 15.67f);
    }
}
