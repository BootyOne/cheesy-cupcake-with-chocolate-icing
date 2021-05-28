using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject hero;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;
    public Animator animator;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public GameObject ElementsList;
    
    public AudioSource HitSound;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (moveInput > 0)
        {
            
            transform.eulerAngles = new Vector3(0, 0, 0);
            ElementsList.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            ElementsList.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping==true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
        }
        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    public void SpeedPlayerUp()
    {
        if (speed == 2)
        {
            speed = (float)(speed * 1.5);
            Invoke("SpeedPlayerDown", 8f);
        }
    }

    private void SpeedPlayerDown()
    {
        speed = (float)(speed * 2 / 3);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Enemy_walking" && EnemyController.canMusic)
        {
            HitSound.Play();
        }
    }
}
    
