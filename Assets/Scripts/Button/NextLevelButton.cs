using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    public void OnClick()
    {   
         Debug.Log(nextLevel);
        SceneManager.LoadScene(nextLevel);

       
    }
}
