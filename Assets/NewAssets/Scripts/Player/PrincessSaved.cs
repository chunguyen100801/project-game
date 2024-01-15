using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessSaved : MonoBehaviour
{
    public GameObject myMenuObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (myMenuObject != null)
            {
                myMenuObject.SetActive(true);
            }
            else
            {
                Debug.Log("Menu object not found!");
            }
        }
    }
}
