using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

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

    public Transform firePoint,anchor;
    public GameObject Rock;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private Rigidbody2D rb;
    private bool suppressingJump=false;
    public bool canJump
    {
        get { return suppressingJump?false: grounded ? true : !doubleJumped; }
        set
        {
            if (value)
            {
                doubleJumped = false;
            }
            else
            {
                if (grounded)
                {
                    grounded = false;
                }
                else
                {
                    doubleJumped = false;
                }
            }
        } }

    //Dash stuff
    public float dashDistance = 700f;
    bool isDashing;
    bool facingRight = true;
    float doubleTapTime;
    KeyCode lastKeycode;


    // Use this for initialization
    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
    }

    async Task SupressJump(int milisec)
    {
        suppressingJump = true;
        await Task.Delay(milisec);
        suppressingJump = false;
    }
    async Task SupressMove(int milisec)
    {
        suppressingMove = true;
        await Task.Delay(milisec);
        suppressingMove = false;
    }
    async Task Dash()
    {
        rb.AddForce(new Vector2(dashDistance, 0) * (facingRight ? 1 : -1),ForceMode2D.Impulse);

        _= SupressMove(5);
        await SupressJump(5);

        rb.AddForce(new Vector2(rb.velocity.x, 0) * -1, ForceMode2D.Impulse);
    }

    [SerializeField] private float maxcoyotetime = 5f;
    [SerializeField]private float currentcoyote = 5f;
    private bool suppressingMove=false;

    bool groundcheck()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckradius, whatIsGround);
    }

    void Jump()
    {
        Task.Run(async () => { await Task.Delay(100);currentcoyote = 0; });
        rb.AddForce(new Vector2(0, Mathf.Clamp(rb.velocity.y, float.NegativeInfinity,0)*-1+jumpHeight), ForceMode2D.Impulse);
        rb.gravityScale = 3;
    }
    void FixedUpdate()
    {
        
        if (groundcheck())
        {
            currentcoyote = maxcoyotetime;
        }
        else
        {
            currentcoyote -= Time.fixedDeltaTime;
        }
        grounded= currentcoyote > 0f;


        if (suppressingMove)
        {
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            moveVelocity = moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            moveVelocity = -moveSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            doubleJumped = !grounded;
            Jump();
            

        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.AddForce(new Vector2(0, -rb.velocity.y / 2), ForceMode2D.Impulse);

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = 5;
        }
        if (rb.velocity.x<0)
        {
            facingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
            anchor.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveVelocity > moveSpeed/5)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
            anchor.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Dash(1f));
            //Task.Run(async()=> { try { await Dash(); } catch (Exception e) { Debug.LogError(e); } });
            Debug.Log("Dash!");
        }
    }


    IEnumerator Dash(float direction)
    {

        rb.AddForce(new Vector2(dashDistance, 0) * (facingRight ? 1 : -1), ForceMode2D.Impulse);

        _ = SupressMove(50);
        _ = SupressJump(50);
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(new Vector2(rb.velocity.x, 0) * -1, ForceMode2D.Impulse);
    }
}
