using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionMenuUI;

    public void Option()
    {
        optionMenuUI.SetActive(true);
    }

    public void Cancel()
    {
        optionMenuUI.SetActive(false);
    }

}
