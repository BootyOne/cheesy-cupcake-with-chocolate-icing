using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovements : MonoBehaviour
{

    public float speed = 2000f;
    public float timeToLive = 5f;
    public Rigidbody2D RockRB;
    public GameObject Rock;
    private GameObject FirePoint;
    private bool isCollided;
    public float checkRadius = 0.1f;
    public LayerMask colliders;
    public int damage;
    private bool canHit =true;
    void Awake()
    {
        FirePoint = GameObject.Find("FirePoint");
    }

    private void Start()
    {
        Vector2 firePointPos = new Vector2(FirePoint.transform.position.x, FirePoint.transform.position.y);
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RockRB.velocity = (mousePos - firePointPos).normalized * speed;
        Invoke("Destroy", timeToLive);
    }

    void Destroy()
    {
        Destroy(Rock);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canHit)
        {
            Health enemy = collision.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(0,0,damage);
            }
        }
        canHit = false;

        Invoke("Destroy",0.5f);
    }
}
