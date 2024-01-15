using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealthPickup : MonoBehaviour
{
    public int healthRestore = 100;

    [SerializeField] private AudioSource healSound;
    // public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    // AudioSource pickupSource;

    // [SerializeField] private AudioSource healthPickupSource;

    private void Awake()
    {
        // pickupSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if (damage && damage.currentHealth < damage.maxHealth)
        {
            bool wasHealed = damage.Heal(healthRestore);

            if (wasHealed)
            {
                // if (pickupSource)
                //     AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);

                // healthPickupSource.Play();
                Destroy(gameObject);
                healSound.Play();
            }

        }
    }

    private void Update()
    {
        // transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
