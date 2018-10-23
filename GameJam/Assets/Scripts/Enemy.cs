using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum ElementalType { Earth, Ice, Fire, Air }
    public ElementalType EnemyType;

    public bool Suckable = false;
    public bool Shrinking = false;
    public bool CanDamage = true;
    public float MoveSpeed = 2.5F;
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D Rigidbody;
    public Collider2D Collider;
    public GameObject Target;
    public Vector3 OriginalScale;
    public float ShrinkDelay = 1.5F;
    public float AggroRange = 5F;
    Vector2 Direction = Vector2.right;
    public float MoveDelay = 0;
    public float DamageDelay = 0;
    public float DamageCooldown = 1F;
    public float PushForce = 10F;

    // Use this for initialization
    void Start ()
    {
        OriginalScale = transform.localScale;
        Collider = GetComponent<Collider2D>();
        Target = GameObject.Find("Player");

        if (!GetComponent<Rigidbody2D>())
        {
            Rigidbody = gameObject.AddComponent<Rigidbody2D>();
        }
        else
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        if (!GetComponent<SpriteRenderer>())
        {
            SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        else
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveToTarget();

        if (Shrinking == true)
        {
            transform.localScale = transform.localScale * 0.9F;
            print("Shrinking");
            gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.transform.position.x, gameObject.transform.position.y), MoveSpeed * Time.deltaTime);
            StartCoroutine(WaitAndDestroy(0.5F));
            //Destroy(gameObject, ShrinkDelay);
        }
        else
        {
            transform.localScale = OriginalScale;
        }

        if (CanDamage == false)
        {
            if (Time.time > DamageDelay)
            {
                CanDamage = true;
            }
        }
	}

    void MoveToTarget()
    {
        // Default Goomba Movement
        //gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.transform.position.x, gameObject.transform.position.y) , MoveSpeed * Time.deltaTime);

        switch (EnemyType)
        {
            case ElementalType.Earth:
                // Earth Movement
                if (Vector2.Distance(Target.transform.position, transform.position) <= AggroRange)
                {
                    gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.transform.position.x, gameObject.transform.position.y), MoveSpeed * Time.deltaTime);
                }
                break;
            case ElementalType.Ice:
                // Ice Movement
                if (Time.time > MoveDelay)
                {
                    transform.Translate(Direction * MoveSpeed * Time.deltaTime);
                    MoveDelay = (Time.time + Random.Range(1, 2));
                    Direction.x *= -1;
                }
                else
                {
                    transform.Translate(Direction * MoveSpeed * Time.deltaTime);
                }
                break;
            case ElementalType.Fire:
                // Fire Movement
                transform.Translate(Direction * MoveSpeed * Time.deltaTime);
                break;
            case ElementalType.Air:
                // Air Movement
                if (Time.time > MoveDelay)
                {
                    transform.Translate(Direction * MoveSpeed * Time.deltaTime);
                    MoveDelay = (Time.time + Random.Range(2,7));
                    Direction.x *= -1;
                }
                else
                {
                    transform.Translate(Direction * MoveSpeed * Time.deltaTime);
                }
                break;
        }
    }

    IEnumerator WaitAndDestroy(float TimeDelay)
    {
        yield return new WaitForSeconds(TimeDelay);
        switch (EnemyType)
        {
            case ElementalType.Fire:
                CurrentElementDisplay.instance.SomethingToFire = true;
                CurrentElementDisplay.instance.EnemyElement = this;
                PlayerController.instance.Projectile = Resources.Load("Prefabs/Bullets/FireBullet") as GameObject;
                break;
            case ElementalType.Earth:
                CurrentElementDisplay.instance.SomethingToFire = true;
                CurrentElementDisplay.instance.EnemyElement = this;
                PlayerController.instance.Projectile = Resources.Load("Prefabs/Bullets/EarthBullet") as GameObject;
                break;
            case ElementalType.Air:
                CurrentElementDisplay.instance.SomethingToFire = true;
                CurrentElementDisplay.instance.EnemyElement = this;
                PlayerController.instance.Projectile = Resources.Load("Prefabs/Bullets/AirBullet") as GameObject;
                break;
            case ElementalType.Ice:
                CurrentElementDisplay.instance.SomethingToFire = true;
                CurrentElementDisplay.instance.EnemyElement = this;
                PlayerController.instance.Projectile = Resources.Load("Prefabs/Bullets/IceBullet") as GameObject;
                break;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player" && CanDamage == true)
        {
            PlayerController.instance.TakeDamage();
            DamageTimer();
            CanDamage = false;
        }

        if (gameObject.tag == "FireElemental" ||  gameObject.tag == "AirElemental") //&& (other.gameObject.tag == "Wall" || other.gameObject.tag == "Player"))
        {
            Direction.x *= -1;
            print("Switch Direction");
        }
    }

    public void DamageTimer()
    {

        DamageDelay = (Time.time + DamageCooldown);
    }
}
