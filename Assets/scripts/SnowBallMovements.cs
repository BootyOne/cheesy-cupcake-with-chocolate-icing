using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallMovements : MonoBehaviour
{
    public float speed = 20f;
    public float timeToLive = 1.5f;
    public Rigidbody2D SnowBallRb;
    public GameObject SnowBall;
    private GameObject FirePoint;
    private bool isCollided;
    public float checkRadius = 0.1f;
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
        SnowBallRb.velocity = (mousePos - firePointPos).normalized * speed;
        Invoke("Destroy", timeToLive);
    }

    void Update()
    {
        isCollided = Physics2D.OverlapCircle(SnowBall.transform.position, checkRadius, colliders);
        if (isCollided)
            Destroy();

    }
    void Destroy()
    {
        Destroy(SnowBall);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        EnemyController enemyController = collision.GetComponent<EnemyController>();
        var batController = collision.GetComponent<EnemyAI>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(0,damage,0);
        }
        if (batController != null)
        {
            batController.Freeze();
        }
        if (enemyController != null)
        {
            enemyController.Freeze();
        }
        Destroy();
    }
}
