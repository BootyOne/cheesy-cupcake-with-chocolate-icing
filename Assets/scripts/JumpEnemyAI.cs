using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JumpEnemyAI : MonoBehaviour
{
    private Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    public int damage = 15;
    public bool canHit = true;
    public float hitDistance = 1f;
    public float speedX = 500f;

    public float speedY = 200f;
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

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Hero").transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < hitDistance && canHit)
        {
            Hit();
        }
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
        Vector2 force = direction * speedY * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            var xVector = new Vector2();
            if (target.position.x < gameObject.transform.position.x)
                xVector = Vector2.left;
            else
                xVector = Vector2.right;
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speedY * jumpModifier + xVector*speedX);
            }
        }

        // Movement


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