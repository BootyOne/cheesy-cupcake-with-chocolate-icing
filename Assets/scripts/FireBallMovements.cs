using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovements : MonoBehaviour
{
    public float speed = 20f;
    public float timeToLive = 1.5f;
    public Rigidbody2D FireBallRb;
    public GameObject Fireball;
    private GameObject FirePoint;
    private bool isCollided;
    public float checkRadius=0.1f;
    public LayerMask colliders;
    public int damage;
    void Awake()
    {
        FirePoint = GameObject.Find("FirePoint");
    }

    private void Start()
    {
        Vector2 firePointPos = new Vector2(FirePoint.transform.position.x, FirePoint.transform.position.y);
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        FireBallRb.velocity = (mousePos - firePointPos).normalized * speed;
        Invoke("Destroy", timeToLive);
    }

    void Update()
    {
        isCollided = Physics2D.OverlapCircle(Fireball.transform.position, checkRadius, colliders);
        if (isCollided)
            Destroy();

    }

    void Destroy()
    {
        Destroy(Fireball);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage,0,0);
        }
        Destroy();
    }
}
