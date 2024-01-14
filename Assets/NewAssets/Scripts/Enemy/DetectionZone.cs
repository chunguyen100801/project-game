using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new();
    public UnityEvent noCollidersRemain;
    Collider2D collier2D;

    private void Awake()
    {
        collier2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);

        if (detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}
