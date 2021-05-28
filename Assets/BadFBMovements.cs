using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadFBMovements : MonoBehaviour
{
    public float speed = 20f;
    public float timeToLive = 1.5f;
    public Rigidbody2D FireBallRb;
    public GameObject Fireball;
    private bool isCollided;
    public float checkRadius = 0.1f;
    public LayerMask colliders;
    public int damage;
    private GameObject Hero;
    private GameObject Boss;
    void Awake()
    {
        Hero = GameObject.Find("Hero");
        Boss = GameObject.Find("boss");
    }

    private void Start()
    {

        FireBallRb.velocity = (Hero.transform.position - Boss.transform.position).normalized * speed;
        Invoke("Destroy", timeToLive);
    }

    void Update()
    {
        var dist = (Hero.transform.position-transform.position).magnitude;
        if (dist < 0.123f)
        {
            var health = Hero.GetComponent<HeroHealth>();
            health.TakeDamage(damage);
            Destroy();
        }
        isCollided = Physics2D.OverlapCircle(Fireball.transform.position, checkRadius, colliders);
        if (isCollided)
            Destroy();

    }

    void Destroy()
    {
        Destroy(Fireball);
    }


}
