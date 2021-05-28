using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemShotMovements : MonoBehaviour
{
    public float speed = 20f;
    public float timeToLive = 5f;
    public Rigidbody2D rb;
    private Transform Golem;
    private GameObject Hero;
    public float checkRadius = 0.1f;
    public float hitDistance;
    public int damage;

    void Awake()
    {
        Hero = GameObject.Find("Hero");
    }

    private void Start()
    {
        Invoke("Destroy", timeToLive);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        var distance = Vector2.Distance(rb.position, Hero.transform.position);
        if (distance < hitDistance)
        {
            Damage();
        }
        else
        {
            Move();
        }
    }
    private void Move()
    {
        var vectorX = Vector2.right;
        var vectorY = Vector2.up;
        if (Hero.transform.position.x < gameObject.transform.position.x)
        {
            vectorX = Vector2.left;
        }
        if (Hero.transform.position.y < gameObject.transform.position.y)
        {
            vectorY = Vector2.down;
        }
        rb.AddForce((vectorX + vectorY) * speed);

    }
    void Damage()
    {
        var herohealth = Hero.GetComponent < HeroHealth>();
        herohealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
