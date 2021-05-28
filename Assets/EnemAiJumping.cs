using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemAiJumping : MonoBehaviour
{
    private Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    public int damage = 15;
    public bool canHit = true;
    public float hitDistance = 1f;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;

    public float activationDist = 2.5f;
    public float activationHeight = 0.7f;
    private bool isActive = false;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Hero").transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            if (TargetInDistance() && followEnabled)
            {
                PathFollow();
            }
        }

    }

    private void Update()
    {
        if (!isActive)
        {
            Activate();
        }
        else
        {
            if (Vector2.Distance(transform.position, target.transform.position) < hitDistance && canHit)
            {
                Hit();
            }
        }

    }
    void Activate()
    {
        if ((target.transform.position - transform.position).magnitude < activationDist && System.Math.Abs(target.transform.position.y - transform.position.y) < activationHeight)
            isActive = true;
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        // Movement
        rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void Hit()
    {
        
        var heroHealth = target.GetComponent<HeroHealth>();
        heroHealth.TakeDamage(damage);
        canHit = false;
        Invoke("MakeAbleToHit", 2);
    }

    public void MakeAbleToHit()
    {
        canHit = true;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}