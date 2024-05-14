using UnityEngine;
using System.Collections;
using System;



public class SamController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    private Animator anim;
    public float jumpForce;
    public bool jump;
    private bool facingRight;
    public Transform groundCheck;
    public Transform projectilespawn;
    public GameObject projectile;
   
    public LayerMask groundLayer;
   // private float groundCheckRadius;
    public bool isGrounded,isAttack,isIdle;

    private float horizontalMotion, verticalMotion;
    private float endTime1,endTime2,endTime3;
    public float duration,duration_power;
    private AudioSource source;
    public AudioClip[] clip;

    // Use this for initialization
    void Start()
    {
        speed = 2f;
        rb = GetComponent<Rigidbody2D>();
        jumpForce = 60;
        jump = false;
        facingRight = true;
        anim = GetComponent<Animator>();
        
        isIdle = true;
        isAttack = false;
        isGrounded = true;
        duration = 5.0f;
        source = GetComponent<AudioSource>();
        duration_power = 30.0f;
        endTime1 = 0;
        endTime2 = 0;
        endTime3 = 0;

    }

    // Update is called once per frame - used for button/key presses
    void Update()
    {
        horizontalMotion = Input.GetAxis("Horizontal");    //get the horizontal motion, if any
        verticalMotion = Input.GetAxis("Vertical");        //get the vertical motion, if any
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            
        }
      
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isAttack = true;
            isIdle = true;
            
           // Instantiate(projectile, projectilespawn.position, Quaternion.identity);
            endTime1 = Time.time;
        }

        if (endTime3 < Time.time)
        {
            jumpForce = 100f;

        }

        if (endTime2 < Time.time)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0);
        }

    }

    void FixedUpdate()
    {
        if (jump && isGrounded)   //space is pressed and the player isn't already jumping and the player is grounded
        {
           
            rb.AddForce(new Vector2(0, jumpForce));    //add the force to have the player jump            

        }
        else if (!isGrounded)
        {
            jump = false;
        }

        if (horizontalMotion > 0f || horizontalMotion < 0f)  //if the player is running
        {
            anim.SetBool("isIdle", false);       //control the animation - change the boolean in the animator
        }
        else
        {
            anim.SetBool("isIdle", true);
        }

        if (isAttack&isIdle&Time.time<=endTime1+duration)
        {
            anim.SetBool("isAttack", true);
            anim.SetBool("isIdle", true);
           

        }else
        {
            anim.SetBool("isAttack", false);
        }

        Console.WriteLine("horizontalMotion");
        Console.WriteLine(facingRight);
        //the following is for flipping the player sprite so it is always facing the right direction
        if (horizontalMotion < 0 && facingRight) //need to change direction
        {
            Flip();
        }
        else if (horizontalMotion > 0 && !facingRight)
        {
            Flip();
        }

        Vector3 motion = new Vector3(speed * horizontalMotion * Time.deltaTime, 0, 0); //standard for vector motion (this is only for x axis)
        this.transform.position = this.transform.position + motion;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
        
    
        anim.SetBool("isGrounded", isGrounded);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 playerScale = this.transform.localScale;
        playerScale.x *= -1;
        this.transform.localScale = playerScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pointUp")
        {
            source.PlayOneShot(clip[0]);
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.tag == "sizeUp")
        {
            source.PlayOneShot(clip[1]);
            Destroy(collision.gameObject);
            endTime2 = Time.time + duration_power;
            transform.localScale = new Vector3(0.5f, 0.5f, 0);
        }

        if(collision.gameObject.tag=="jumpUp")
        {
            source.PlayOneShot(clip[1]);
            Destroy(collision.gameObject);
            endTime3 = Time.time + duration_power;
            jumpForce=2*jumpForce;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttack == true&&collision.gameObject.tag=="robot")
        {
            source.PlayOneShot(clip[2]);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag=="robot")
        {
            source.PlayOneShot(clip[3]);
        }

        if(collision.gameObject.tag=="sharp")
        {
            source.PlayOneShot(clip[3]);
        }
        
    }

}
