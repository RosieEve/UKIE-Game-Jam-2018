using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    public LayerMask EnemyLayer;
    public List<RaycastHit2D> Hits = new List<RaycastHit2D>();
    public float VacuumDelay;
    public float VacuumCooldown = 0.1F;

    public GameObject VacuumLaser;

    private void Awake()
    {
        VacuumLaser.SetActive(false);
    }

    void Update()
    {
        Suck();
        VacuumAnimation();
    }
    void Suck()
    {
        if (Input.GetButton("Fire1"))
        {
            VacuumDelay = (Time.time + VacuumCooldown);
            Hits.Clear();

            if (PlayerController.instance.Projectile == null)
            {
                Debug.Log("Test");
                Vector2 Position = new Vector2(transform.position.x, transform.position.y);
                Vector2 Direction;
                if (PlayerController.instance._Direction == false)
                {
                    Direction = Vector2.left;
                }
                else
                {
                    Direction = Vector2.right;
                }

                float CastDistance = 1F;

                RaycastHit2D Hit = Physics2D.Raycast(Position, Direction, CastDistance, EnemyLayer);
                Debug.DrawRay(Position, Direction, Color.red);
                if (Hit.collider != null)
                {
                    Debug.Log("Test2");
                    Hits.Add(Hit);

                    foreach (RaycastHit2D ObjectHit in Hits)
                    {
                        if (Hit.transform.gameObject.GetComponent<Enemy>())
                        {
                            Hit.transform.gameObject.GetComponent<Enemy>().MoveSpeed = 0.1F;
                            Hit.transform.position = Vector2.MoveTowards(Hit.transform.position, transform.position, Hit.transform.gameObject.GetComponent<Enemy>().MoveSpeed * Time.deltaTime);
                            Hit.transform.gameObject.GetComponent<Enemy>().Shrinking = true;
                            Hit.transform.gameObject.GetComponent<Enemy>().DamageTimer();
                            Hit.transform.gameObject.GetComponent<Enemy>().CanDamage = false;
                        }
                    }              
                }
                //}              

                //if (Hit.collider != null)
                //{
                //    if (Hit.transform.gameObject.GetComponent<Enemy>())
                //    {
                //        Hit.transform.gameObject.GetComponent<Enemy>().MoveSpeed = 0.1F;
                //        Hit.transform.position = Vector2.MoveTowards(Hit.transform.position, transform.position, Hit.transform.gameObject.GetComponent<Enemy>().MoveSpeed * Time.deltaTime);
                //        Hit.transform.gameObject.GetComponent<Enemy>().Shrinking = true;
                //        Hit.transform.gameObject.GetComponent<Enemy>().CanDamage = false;
                //    }
            }            
        }
    }

    void VacuumAnimation()
    {
        if (Input.GetButton("Fire1"))
        {
            VacuumLaser.SetActive(true);
        }
        else
        {
            VacuumLaser.SetActive(false);
        }
    }
}
