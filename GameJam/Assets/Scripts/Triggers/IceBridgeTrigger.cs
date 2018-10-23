using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBridgeTrigger : MonoBehaviour
{
    public GameObject[] PlatformsToEnable;
    // Use this for initialization
    void Start ()
    {
        foreach (GameObject platform in PlatformsToEnable)
        {
            platform.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.tag == "IceBullet")
        { 
            foreach (GameObject platform in PlatformsToEnable)
            {
                platform.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
