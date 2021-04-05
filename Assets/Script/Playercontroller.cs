using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    public static Playercontroller player;
    public float moveSpeed;
    private float moveVelocity;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckradius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJumped;

    public Transform firePoint;
    public GameObject Rock;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private Rigidbody2D rb;



    //Dash stuff
    public float dashDistance = 7f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeycode;


    // Use this for initialization
    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckradius, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {

        if (grounded)
        {
            doubleJumped = false;
        }

        if (GetComponent<Rigidbody2D>().velocity.x > 0)

            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        moveVelocity = 0f;
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            doubleJumped = true;
        }
        //moveVelocity = moveSpeed = Input.GetAxisRaw("Horizontal"); 


        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = -moveSpeed;
        }


        //Dash
        //sol
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (doubleTapTime > Time.time && KeyCode.A == lastKeycode)
            {
                StartCoroutine(Dash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.2f;
            }

            lastKeycode = KeyCode.A;
        }

        //sag
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && KeyCode.D == lastKeycode)
            {
                StartCoroutine(Dash(1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.2f;
            }

            lastKeycode = KeyCode.D;
        }
    }

    IEnumerator Dash (float direction) {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        rb.gravityScale = 3;
    }
}
