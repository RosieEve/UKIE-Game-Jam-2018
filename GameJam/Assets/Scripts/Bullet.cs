using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float Speed = 5F;
    public float BulletLife = 2F;
    public Collider2D Collider;
    public Rigidbody2D RigidBody;
    private SpriteRenderer sprite;
    public GameObject Elemental;

    // Use this for initialization
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        if (!GetComponent<Collider2D>())
        {
            Collider = gameObject.AddComponent<Collider2D>();
            Collider.isTrigger = true;
        }
        else
        {
            Collider = GetComponent<Collider2D>();
        }
        BulletLife = Time.time + BulletLife;
    }
    private void Update()
    {
        RigidBody.velocity = transform.right * Speed;

        if (Time.time > BulletLife)
        {
            SpawnElemental(transform.position);
            Destroy(gameObject);
        }
    }

    public void SpawnElemental(Vector3 Position)
    {
        Instantiate(Elemental, Position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SpawnElemental(transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != null)
        {
            SpawnElemental(transform.position);
            Destroy(gameObject);
        }
    }

    IEnumerator WaitAndSpawn(float WaitTime)
    {
        sprite.enabled = false;
        Collider.enabled = false;
        RigidBody.isKinematic = true;
        RigidBody.velocity = Vector2.zero;
        Vector2 ImpactPosition = transform.position;
        yield return new WaitForSeconds(WaitTime);
        SpawnElemental(transform.position);
        Destroy(gameObject);
    }
}
