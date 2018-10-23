using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ElementalType { Earth, Ice, Fire, Air }
    public ElementalType ProjectileType;

    public GameObject ProjectilePrefab;
    public float Speed = 5F;
    public Collider2D Collider;

    // Use this for initialization
    void Start () 
    {
        if (!GetComponent<Collider2D>())
        {
            Collider = gameObject.AddComponent<Collider2D>();
            Collider.isTrigger = true;
        }
        else
        {
            Collider = GetComponent<Collider2D>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D Other)
    {
         
    }


    public void IceProjectile()
    {

    }
}
