using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int countdown;
    public Text canvasCountdown;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown()
    {
        while (countdown > 0)
        {
            canvasCountdown.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        canvasCountdown.text = "GO!";

        FindObjectOfType<Game>().BeginGame();
        FindObjectOfType<SpawnTetromino>().NewTetromino();
        FindObjectOfType<AudioManager>().PlayAudio();

        yield return new WaitForSeconds(1f);
        canvasCountdown.gameObject.SetActive(false);
    }
}
