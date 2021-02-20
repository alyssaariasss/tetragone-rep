using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector2 previewBlockPos = new Vector2(14, 15);
    private Vector2 savedBlockPos = new Vector2(-7, 15);

    public int maxSwap = 3;
    private int swapCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
    }

    public void NewTetromino()
    {
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
            previewBlock.transform.localPosition = new Vector2(4.0f, 20.0f);
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
}
