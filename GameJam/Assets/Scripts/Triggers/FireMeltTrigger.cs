using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMeltTrigger : MonoBehaviour
{
    public GameObject[] WallsToMelt;

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.tag == "FireElemental")
        {
            StartCoroutine(WaitAndDestroy(2F));
        }
    }

    IEnumerator WaitAndDestroy(float TimeDelay)
    {
        yield return new WaitForSeconds(TimeDelay);
        foreach (GameObject Wall in WallsToMelt)
        {
            Wall.SetActive(false);
        }
        Destroy(gameObject);
    }
}
