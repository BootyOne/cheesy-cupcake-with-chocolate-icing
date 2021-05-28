using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    private Transform Hero;
    public float speed;
    public int damage;
    public float stopDistance;
    public Rigidbody2D rb;
    public GameObject GolemShot;
    private bool canShoot = true;

    
    void Start()
    {
        Hero = GameObject.Find("Hero").transform;
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var distanceToHero = Vector2.Distance(transform.position, Hero.transform.position);
        if (distanceToHero > stopDistance)
        {
            Move();
            
        }
        else
        {
            if (canShoot)
            {
                canShoot = false;
                Shoot();
                Invoke("MakeAbleToShoot", 3f);
            }
        }
    }
    private void MakeAbleToShoot()
    {
        canShoot = true;
    }
    private void Move()
    {
        if (Hero.position.x > gameObject.transform.position.x)
        {
            rb.AddForce(Vector2.right * speed);
        }
        else
        {
            rb.AddForce(Vector2.left * speed);
        }
    }

    private void Shoot()
    {
        Instantiate(GolemShot, transform.position, transform.rotation);
    }
}
