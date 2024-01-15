using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonDoor : MonoBehaviour
{
    public GameObject victoryMenu;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Enemy");

        int aliveSoldiers = soldiers.Length;

        Debug.Log("Enemy count = " + aliveSoldiers);

        if (aliveSoldiers == 0)
        {
            animator.SetTrigger("openDoor");
            var prison = GameObject.FindGameObjectsWithTag("Prison");
            Destroy(prison[0], 2f);
            if (victoryMenu != null)
            {
                StartCoroutine(ShowMenuAfterDelay(3f));
            }
            else
            {
                Debug.Log("Menu object not found!");
                // Handle the case where the object wasn't found
            }
        }

    }
    IEnumerator ShowMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        victoryMenu.SetActive(true);
    }
}
