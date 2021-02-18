using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static int height = 20;
    private static int width = 10;

    private static Transform[,] grid = new Transform[width, height];

    // checks if block is outside grid limits
    public bool IsAboveGrid(TetroBlock tetroblock)
    {
        for (int x = 0; x < width; ++x)
        {
            foreach (Transform block in tetroblock.transform)
            {
                Vector2 pos = Round(block.position);
                if (pos.y > (height - 1))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
