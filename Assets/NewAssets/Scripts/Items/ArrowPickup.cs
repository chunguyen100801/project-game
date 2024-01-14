using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    // AudioSource pickupSource;
    [SerializeField] private AudioSource healthPickupSource;
    Player player;
    public int arrowCount = 5;
    private void Awake()
    {
        // pickupSource = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();
        player = collision.GetComponent<Player>();

        if (damage && damage.isAlive)
        {
            player.arrowCount += arrowCount;
            player.arrowCountText.text = player.arrowCount.ToString();
            // if (pickupSource)
            //     AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);

            // healthPickupSource.Play();
            Destroy(gameObject);
        }
    }
}
