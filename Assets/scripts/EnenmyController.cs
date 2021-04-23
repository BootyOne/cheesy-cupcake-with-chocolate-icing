using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyController : MonoBehaviour
{
    public bool MoveLeft = true;
    [SerializeField] private float speed;
    [SerializeField] private float speedBoost;
    [SerializeField] private float distanceAngry;
    [SerializeField] private float distancePatrol;
    private float minDistance;
    private float maxDistance;
    private Rigidbody2D rb;
    private bool patrol = true;
    private Transform player;

    private Component[] Joints;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero").transform;
        rb = GetComponent<Rigidbody2D>();
        var position = transform.position;
        maxDistance = position.x + distancePatrol;
        minDistance = position.x - distancePatrol;

        Joints = gameObject.GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol)
            Patrol();
        else
        {
            Angry();
        }
        if (Vector2.Distance(transform.position, player.position) < distancePatrol)
        {
            speed = Math.Abs(speed);
            patrol = false;
        }
    }

    public void Patrol()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        if (transform.position.x > maxDistance)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (transform.position.x < minDistance)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    private void Angry()
    {
        if (!patrol)
        {
            Vector2 moveVector =
                Vector2.MoveTowards(transform.position, player.position, speedBoost * speed * Time.deltaTime);
            transform.position = new Vector3(moveVector.x, transform.position.y);
            if (transform.position.x > player.position.x)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else if (transform.position.x < player.position.x)
            {
                transform.rotation = Quaternion.Euler(0,180,0);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Hero")
            BaseStatic.DeadHero = true;
        else if (other.name == "legs")
        {
            Destroy(gameObject);
        }
    }
}
