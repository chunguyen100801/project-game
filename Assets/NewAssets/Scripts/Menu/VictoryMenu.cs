using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public void Finish()
    {
        Application.Quit();
    }

    public void PLayAgain()
    {
        SceneManager.LoadScene("Menu-Main");
    }
}
