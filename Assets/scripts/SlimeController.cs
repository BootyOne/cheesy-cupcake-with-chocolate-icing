using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private Transform Hero;
    public Rigidbody2D rb;
    public float speedX;
    public float speedY;
    private bool canJump=true;
    public bool canHit = true;
    public float hitDistance = 1f;
    public int damage = 20;

    public float activationDist = 2.5f;
    public float activationHeight = 0.7f;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Hero = GameObject.Find("Hero").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(isActive))
        {
            Activate();
        }
        else
        {
            if (Vector2.Distance(transform.position, Hero.transform.position) < hitDistance && canHit)
            {
                Hit();
            }
            if (canJump)
            {
                canJump = false;
                Jump();
                Invoke("MakeAbleToJump", 3f);
            }
        }
    }

    void Activate()
    {
        if ((Hero.transform.position - transform.position).magnitude < activationDist && System.Math.Abs(Hero.transform.position.y - transform.position.y) < activationHeight)
            isActive = true;
    }
    void Jump()
    {
        if (Hero.position.x > gameObject.transform.position.x)
        {
            rb.AddForce(Vector2.up * speedX + Vector2.right * speedX);
        }
        else
        {
            rb.AddForce(Vector2.up * speedX + Vector2.left * speedX);
        }
    }
    private void Hit()
    {

        var heroHealth = Hero.GetComponent<HeroHealth>();
        heroHealth.TakeDamage(damage);
        canHit = false;
        Invoke("MakeAbleToHit", 2);
    }

    public void MakeAbleToHit()
    {
        canHit = true;
    }
    void MakeAbleToJump()
    {
        canJump = true;
    }
}
