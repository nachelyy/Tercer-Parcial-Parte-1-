using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// intento 3


public class Player : MonoBehaviour
{
    public static Player obj;

    public int lives = 3;

    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isImmune = false;

    public float speed = 5f;
    public float jumpForce = 10f;
    public float movHor;

    public float immuneTimeCnt = 0f;
    public float immuneTime = 0.5f;

    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

   
    void Awake()//si
    {
        obj = this;
    }


  


    

    // Start is called before the first frame update
    void Start()//si
    {
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       spr = GetComponent<SpriteRenderer>();
    }

    





    // Update is called once per frame
    void Update()
    {
      //  if (Game.obj.gamePaused)
      //  {
      //      movHor = 0f;
     //       return;
      //  }

      movHor = Input.GetAxisRaw("Horizontal");//

      isMoving = (movHor != 0f);

      isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

  if (Input.GetKeyDown(KeyCode.Space))
           jump();
        /* 
         if (isImmune)
         {
             spr.enabled = !spr.enabled;

             immuneTimeCnt -= Time.deltaTime;

             if (immuneTimeCnt <= 0)
             {
                 isImmune = false;
                 spr.enabled = true;
             }
         }*/

       anim.SetBool("isMoving", isMoving);
       anim.SetBool("isGrounded", isGrounded);

       flip(movHor);
    }








    void FixedUpdate()//si
    {
     rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }




  

    private void goImmune()
    {
      //  isImmune = true;
       // immuneTimeCnt = immuneTime;
    }



    public void jump()
    {
      if (!isGrounded) return;

      rb.velocity = Vector2.up * jumpForce;
      //  AudioManager.obj.playJump();
    }



    private void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue < 0)
            theScale.x = Mathf.Abs(theScale.x) * -1;
        else
        if (_xValue > 0)
            theScale.x = Mathf.Abs(theScale.x);

        transform.localScale = theScale; 
    }






    
    public void getDamage()
    {
        lives--;
       // AudioManager.obj.playHit();

      //  UIManager.obj.updateLives();

      //  goImmune();

        if (lives <= 0)
        {
            //  FXManager.obj.showPop(transform.position);
            // Game.obj.gameOver();


          this.gameObject.SetActive(false); 


        }

    }

    
    public void addLive()
    {
      lives++;

        if (lives > Game.obj.maxLives)
            lives = Game.obj.maxLives;
    }

    
    


    void OnDestroy()// si
    {
      obj = null;
    }
}
