using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject myMenuObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Enemy");

        int aliveSoldiers = soldiers.Length;

        Debug.Log("Enemy count = " + aliveSoldiers);

        if (aliveSoldiers == 0)
        {
            if (myMenuObject != null)
            {
                // Proceed with your code using myMenuObject
                myMenuObject.SetActive(true);
            }
            else
            {
                Debug.Log("Menu object not found!");
                // Handle the case where the object wasn't found
            }
        }


    }
}
