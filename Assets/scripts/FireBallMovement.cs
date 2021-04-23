using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject FirePoint;
    public GameObject FireBall;
    public float lifeTime = 1.5f;
    private bool isCollided;
    public Rigidbody2D ball;
    public float checkRadius;
    public LayerMask whatIsGround;

    private void Awake()
    {
        FirePoint = GameObject.Find("FirePoint");
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", lifeTime);

        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 FirePointPos = new Vector2(FirePoint.transform.position.x, FirePoint.transform.position.y);
        rb.velocity = (mousePosition-FirePointPos).normalized * speed;
    }
    private void Update()
    {
        isCollided = Physics2D.OverlapCircle(ball.position, checkRadius, whatIsGround);
        if (isCollided) Destroy(FireBall);
    }
    private void Destroy()
    {
        Destroy(FireBall);
    } 
}
