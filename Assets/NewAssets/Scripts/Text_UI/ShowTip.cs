using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogBox;
    public AudioSource showDialogEffect;
    void Start()
    {
        dialogBox.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            dialogBox.SetActive(true);
         //   showDialogEffect.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            dialogBox.SetActive(false);
        }
    }
}
