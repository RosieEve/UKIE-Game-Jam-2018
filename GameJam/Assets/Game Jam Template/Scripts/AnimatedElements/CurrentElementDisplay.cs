using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentElementDisplay : MonoBehaviour {

    public Sprite[] ElementSprites;  //0 --> Air, 1 --> water, 2--> Earth, 3--> Fire, 4 --> empty
    private Image sprite;

	// Use this for initialization
	void Start () {

        sprite = gameObject.GetComponent<Image>();
        sprite.sprite = ElementSprites[4];
        sprite.color = Color.white;

    }
	
	// Update is called once per frame
	void Update () {

        CurrentElement();

	}


    void CurrentElement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sprite.sprite = ElementSprites[0];
            sprite.color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sprite.sprite = ElementSprites[1];
            sprite.color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            sprite.sprite = ElementSprites[2];
            sprite.color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            sprite.sprite = ElementSprites[3];
            sprite.color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            sprite.sprite = ElementSprites[4];
            sprite.color = Color.white;
        }
    }
}
