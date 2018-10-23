using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentElementDisplay : MonoBehaviour {

    public  static CurrentElementDisplay instance;

    public Sprite[] ElementSprites;  //0 --> Air, 1 --> water, 2--> Earth, 3--> Fire, 4 --> empty
    private Image sprite;
    public Enemy EnemyElement;
    public bool SomethingToFire = false;

    private void Awake()
    {
        instance = this;
    }

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
        if (PlayerController.instance.Projectile == null && SomethingToFire == false)
        {
            sprite.sprite = ElementSprites[4];
            sprite.color = Color.white;
        }
        else if (EnemyElement.EnemyType == Enemy.ElementalType.Air)
        {
            sprite.sprite = ElementSprites[0];
            sprite.color = Color.white;
        }
        else if (EnemyElement.EnemyType == Enemy.ElementalType.Ice)
        {
            sprite.sprite = ElementSprites[1];
            sprite.color = Color.white;
        }
        else if (EnemyElement.EnemyType == Enemy.ElementalType.Earth)
        {
            sprite.sprite = ElementSprites[2];
            sprite.color = Color.white;
        }
        else if (EnemyElement.EnemyType == Enemy.ElementalType.Fire)
        {
            sprite.sprite = ElementSprites[3];
            sprite.color = Color.white;
        }
    }
}
