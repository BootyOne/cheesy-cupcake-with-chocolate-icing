using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    public Transform enemyGraphics;
    private GameObject hero;
    bool reachedEndOfPath = false;
    public int damage = 20;
    Seeker seeker;
    Rigidbody2D rb;

    private bool canHit = true;
    public float activationDist = 2.5f;
    public float activationHeight = 0.7f;
    private bool isActive = false;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        hero = GameObject.Find("Hero");
        target = hero.transform;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void Activate()
    {
        if ((hero.transform.position - transform.position).magnitude < activationDist && System.Math.Abs(hero.transform.position.y - transform.position.y) < activationHeight)
            isActive = true;
    }
    private void Update()
    {
        if (!isActive)
        {
            Activate();
        }
        else
        {
            if (DistanceToHero() < 0.27f && canHit)
            {
                canHit = false;
                Invoke("MakeAbleToHit", 2f);
                var heroHealth = hero.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(damage);
            }
        }

    }

    private void MakeAbleToHit()
    {
        canHit = true;
    }

    private double DistanceToHero()
    {
        return System.Math.Sqrt(System.Math.Pow((hero.transform.position.x - gameObject.transform.position.x),2) +
            System.Math.Pow((hero.transform.position.y - gameObject.transform.position.y) * (hero.transform.position.y - gameObject.transform.position.y),2));
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (force.x >= 0.01f)
            {
                enemyGraphics.localScale = new Vector3(-1f, 1f, 1f);

            }
            else if (force.x <= -0.01f)
            {
                enemyGraphics.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        
    }

    public void Freeze()
    {
        speed *= 0.8f;
        Invoke("ReturnSpeed", 2);
    }


    public void ReturnSpeed()
    {
        speed *= 1.25f;
    }
}
