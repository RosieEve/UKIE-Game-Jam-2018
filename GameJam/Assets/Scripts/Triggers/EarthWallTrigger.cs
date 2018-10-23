using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.tag == "EarthBullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "EarthBullet")
        {
            Destroy(gameObject);
        }
    }
}
