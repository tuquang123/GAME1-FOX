using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    #region Variable
    [Header("1-OBJECT")]

    public ParticleSystem Dust; //Dush
    public Joystick joystick; //joystick
    public GameObject IsDash; //off
    [Space]
    //start () variable : bien khoi tao
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D Collider;
    private float naturalGravity;

    //FSM : enum trang thai 
    private enum State { idle, running, jumping, falling , hurt ,climb }
    private State state = State.idle;

    //ladder
    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool bottomLadder = false;
    [HideInInspector] public bool topLadder = false;
    [HideInInspector] public Ladder ladder;

    //Imspector variable : bien co the tinh chinh
    [SerializeField] private LayerMask ground;
    [Space]
    [Header("2-SPEED")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private const float hurtForce = 18f;
    [SerializeField] private float climSpeed = 3f;
    public float upspeed = 0;
    //Dash 
    public float dashSpeed;
    [HideInInspector]private float dashTime;
    [HideInInspector] public float startDashTime;
    private int direction;
    [HideInInspector] public bool Cr = false;
    [HideInInspector] public bool Jumpx2;
    [Space]
    //Audio
    [Header("3-AUDIO")]
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource up;
    [SerializeField] private AudioSource jump;
    #endregion //bien khoi tao //bien khoi tao

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Rigidbody
        anim = GetComponent<Animator>();  // Animation
        Collider = GetComponent<BoxCollider2D>();   // Boxcollider
        footstep = GetComponent<AudioSource>();
    }


    private void Start()
    {
        dashTime = startDashTime;
        PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
        naturalGravity = rb.gravityScale;
    }

    private void Update()
    {
        if (Collider.IsTouchingLayers(ground))
        {
            upspeed = 500f;
        }
        if (state == State.climb)
        {
            Climb();
        }
        else if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //set animation trong enum state : dat cac trang thai trong enum animation
    }
    
    #region TOUCH
    //Touch Tag
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.tag == "Conllectable")// play sound + destroy + 1 Mr + dispaly Mr
        {
            cherry.Play();
            Destroy(collison.gameObject);
            PermanentUI.perm.Mushroom += 1;
            PermanentUI.perm.MushroomText.text = PermanentUI.perm.Mushroom.ToString();
        }
        if(collison.tag == "Up") // if collison destroy + jumforce  10 -> 35 , change color -> red ;
        {
            up.Play();
            Destroy(collison.gameObject);
            jumpForce = jumpForce + 10f;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ResetPower());
        }
    }
    
    //Touch Enemy
    private void OnCollisionEnter2D(Collision2D other)
    { 
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpOn();
                Jump();
            }
            else
            {
                {
                    state = State.hurt;
                    HandleHeath(); // -heath , display heath , reset heath if = 0 
                    if (other.gameObject.transform.position.x > transform.position.x)
                    {
                        rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    }
                }
            }
            
        }
        //Touch Springs
        if (other.gameObject.tag == "Trap" && !Collider.IsTouchingLayers(ground))
        {
            
            upspeed += 300f;
            if(upspeed >= 2000f)
            {
                upspeed = 2000f;
            }
            rb.AddForce(new Vector2(0, upspeed));
        }
        //Touch Bullet
        if (other.gameObject.tag == "Bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            bullet.OnBullet();
            state = State.hurt;
            HandleHeath(); 
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
        }
        
    }
    #endregion
    // -heath , display heath , reset heath if = 0
    private void HandleHeath()
    {
        PermanentUI.perm.health -= 1;
        PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
        if (PermanentUI.perm.health <= 0)
        {
            anim.SetTrigger("Death");
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }
    private void Death()
    {
        PermanentUI.perm.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #region Move
    private void Movement() //Move
    {
        float hDirection;
        if (joystick.Horizontal >= .2f)
        {
            hDirection = speed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            hDirection = -speed;
        }
        else
        {
            hDirection = 0f;
        }
        if (canClimb)
        {
            state = State.climb;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(ladder.transform.position.x, rb.position.y);
            rb.gravityScale = 0f;
        }
        //Moving Left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        //Moving Right
        else if (hDirection > 0)
        {
            
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        //Junging
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (Collider.IsTouchingLayers(ground))
            {    
                Jump();
                Jumpx2 = true;         
            }
            else
            {
                //Jumpx2
                if (Jumpx2)
                {
                    CreateDust();
                    Jumpx2 = false;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    Jump();
                }
            }
            
        }
        //Crouch
        if (CrossPlatformInputManager.GetButtonDown("Crouch"))
        {
            anim.SetBool("Cr", true);
            speed = 4f;
            return;
        }

        if (CrossPlatformInputManager.GetButtonUp("Crouch"))
        {
            anim.SetBool("Cr", false);
            speed = 10f;
        }
        Dash();
    }
    #endregion
    //Dash
    private void Dash()
    {
        if (direction == 0)
        {
            if (CrossPlatformInputManager.GetButtonDown("Dash"))
            {
                direction = 2;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 2)
                {
                    CreateDust();
                    rb.velocity = Vector2.right * dashSpeed;
                    anim.SetTrigger("Dash");
                    IsDash.SetActive(false);
                    StartCoroutine(ResetDash());
                }
            }
        }
    }
    //Jump
    protected virtual void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
   
    }
    #region Animation
    //Animation 
    private void AnimationState()
    {
        
        if (state == State.climb)
        {

        }
        else if (state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if (Collider.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
    #endregion
    private void Footstep() // footstep play 
    {
        footstep.Play();
    }
    private void Jumping() // jump play 
    {
        this.jump.Play();
    }
    // creater dust
    void CreateDust()
    {
        Dust.Play();
    }

    private IEnumerator ResetPower()// wait 10s return entry
    {
        yield return new WaitForSeconds(10);
        jumpForce = 35;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(3);
        IsDash.SetActive(true);
    }

    //Climb
    private void Climb()
    {
        float vDirection = joystick.Vertical;
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Jump();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            rb.gravityScale = naturalGravity;
            anim.speed = 1f;
            
        }
        //climb up
        if(vDirection >= .5f && !topLadder)
        {
            rb.velocity = new Vector2(0f, vDirection * climSpeed);
            anim.speed = 1f;
        }
        //climb down
        else if (vDirection <= .5f && !bottomLadder)
        {
            rb.velocity = new Vector2(0f, vDirection * climSpeed);
            anim.speed = 1f;
        }
        //pause
        

    }

    
}
