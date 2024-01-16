using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {


    }
    void Update()
    {

    }

    public void playSound()
    {
        audioSource.Play();
    }
}
