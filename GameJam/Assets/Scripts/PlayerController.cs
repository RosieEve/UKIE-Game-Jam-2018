using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // Movement Variables
    [SerializeField] private float m_JumpForce = 400f; // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // How much to smooth out the movement
    private Rigidbody2D m_Rigidbody2D;
    public bool _Direction = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character
    public bool AirControl = false; // Do we have control of the character mid air.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded; // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    // Combat Variables
    public int Hitpoints = 3;
    public GameObject Projectile;
    public Vector2 StartPosition;
    public Transform Firepoint;
    public float FireDelay;
    public float FireCooldown = 0.5F;
    public SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        instance = this;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        StartPosition = transform.position;
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Hitpoints <= 0)
        {
            Die();
        }

        Fire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1, m_WhatIsGround);

        if (ray == true)
        {
            m_Grounded = true;
        }
        Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
    public void TakeDamage()
    {
        StartCoroutine(CTakeDamage(0.5F));
    }

    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !_Direction)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && _Direction)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            if (Projectile != null && Projectile.name == "AirBullet")
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * 2));                
                StartCoroutine(SpawnElemental(1F));
            }
            else
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        _Direction = !_Direction;

        transform.Rotate(0,180,0);
    }
    void Die()
    {
        Hitpoints = 3;
        transform.position = StartPosition;
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1") && Projectile != null)
        {
            Instantiate(Projectile, Firepoint.transform.position, Firepoint.rotation);
            Projectile = null;
            CurrentElementDisplay.instance.SomethingToFire = false;
        }        
    }

    public IEnumerator CTakeDamage(float TimeDelay)
    {
        Hitpoints--;
        //m_Rigidbody2D.AddForce(new Vector2(-100, 0), ForceMode2D.Impulse);
        SpriteRenderer.enabled = false;
        yield return new WaitForSeconds(TimeDelay);
        SpriteRenderer.enabled = true;
    }

    public IEnumerator SpawnElemental(float TimeDelay)
    {
        yield return new WaitForSeconds(TimeDelay);
        Projectile.GetComponent<Bullet>().SpawnElemental(new Vector2(transform.position.x, transform.position.y - 3));
        Projectile = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            StartPosition = transform.position;
            print("CHECKPOINT");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            StartPosition = transform.position;
            print("CHECKPOINT");
        }
    }
}
