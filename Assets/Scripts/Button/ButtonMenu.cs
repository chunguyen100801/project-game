using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class ButtonMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
