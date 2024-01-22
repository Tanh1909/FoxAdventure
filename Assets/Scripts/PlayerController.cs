using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Vector2 startPoint;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    public Vector2 debug;
    [SerializeField] private Text cherriesText;

    
    public Animator animator;
    public int cherries = 0;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isRunning = false;
    public bool isHurted=false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    //cherry triggle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {

            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }
    //enemy triggle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FrogController frog=collision.gameObject.GetComponent<FrogController>();
            if (isFalling)
            {
                frog.Triggle();
                
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
               
            }
            else
            {
                isHurted = true;
            }
     
        }
    }
    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Start()
    {
        startPoint= transform.position;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
      
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (rb.velocity.y < 0f && !IsGrounded())
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
     
        //kiem tra co dang chay khong
        isRunning = Mathf.Abs(horizontal) > 0f;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("isHurted", isHurted);
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        debug = rb.velocity;
        isJumping = !IsGrounded();
        if (isFalling && IsGrounded())
        {
            isFalling = false; 
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
}
