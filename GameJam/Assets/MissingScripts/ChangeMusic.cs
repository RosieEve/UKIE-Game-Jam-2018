using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour {

    public GameObject camera;
    private AudioSource source;
    public AudioClip Clip;

	// Use this for initialization
	void Start () {

        source = camera.GetComponent<AudioSource>();

		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            source.clip = Clip;
            source.Play();


            Destroy(gameObject);
        }
    }
}
