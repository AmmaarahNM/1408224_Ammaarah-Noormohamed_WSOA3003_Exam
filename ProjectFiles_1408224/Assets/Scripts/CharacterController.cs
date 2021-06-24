using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Transform startPos;
    Rigidbody2D playerRB;
    Animator animator;

    public int speed;
    public int jumpValue;
    public bool isGrounded;

    public int health;

    public GameManager GM;

    public ParticleSystem dust;
    ParticleSystem.EmissionModule dustEmission;
    public bool canEatAcorns = false;
    public bool acornIngested = false;
    public bool isDrowning;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = startPos.position;
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = 4;
        dustEmission = dust.emission;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isGrounded)
        {
            if ((Input.GetKeyDown(KeyCode.Space))) //GET KEY INSTEAD OF GET KEY DOWN????
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpValue);
                animator.SetInteger("AnimState", 2);
                //dust.Play();
                if (acornIngested)
                {
                    playerRB.velocity = new Vector2(playerRB.velocity.x, 10);
                    animator.SetInteger("AnimState", 2);
                    acornIngested = false;
                    //deactivateUI
                }

            }


            if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
            {
                //dust.Play();
                playerRB.velocity = new Vector2(-speed, playerRB.velocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
                animator.SetInteger("AnimState", 1);
                
            }

            else if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
            {
                //dust.Play();
                playerRB.velocity = new Vector2(speed, playerRB.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
                animator.SetInteger("AnimState", 1);
                
            }

            else
            {
                animator.SetInteger("AnimState", 0);
                playerRB.velocity = new Vector2(0, playerRB.velocity.y);
                //dust.Stop();
            }



        }

        else
        {
            //make KINEMATIC here!
            animator.SetInteger("AnimState", 2);
            if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
            {
                playerRB.velocity = new Vector2(-speed, playerRB.velocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            else if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
            {
                playerRB.velocity = new Vector2(speed, playerRB.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if ((Input.GetAxisRaw("Horizontal") != 0) && isGrounded)
        {
            dustEmission.rateOverTime = 20f;
        }

        else
        {
            dustEmission.rateOverTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && canEatAcorns == true && acornIngested == false && GM.acorns > 0)
        {
            acornIngested = true;
            GM.acorns--;
            //activate UI to say you've eaten an acorn
            Debug.Log("eaten an acorn");
        }

        if (isDrowning == true)
        {
            GM.LoseHealth();
        }

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingPlatform")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingPlatform")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(collision.gameObject.transform);
        }

            if (collision.gameObject.tag == "Fox") // also if grounded?
        {
            GM.LoseHealth();
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                //transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                playerRB.velocity = new Vector2(-3, playerRB.velocity.y);

            }

            else
            {
                //transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                playerRB.velocity = new Vector2(3, playerRB.velocity.y);

            }
        }

        if (collision.gameObject.name == "Lake")
        {
            isDrowning = true;
        }

        if (collision.gameObject.tag == "Fox")
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                playerRB.velocity = new Vector2(-3, playerRB.velocity.y);

            }

            else
            {
                playerRB.velocity = new Vector2(3, playerRB.velocity.y);

            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingPlatform")
        {
            isGrounded = false;
        }

        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
        }

        if (collision.gameObject.name == "Lake")
        {
            isDrowning = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acorn")
        {
            if (GM.acorns < 25)
            {
                AcornValue AV = collision.gameObject.GetComponent<AcornValue>();
                int elem = AV.GetValue();
                //put in array
                GM.PickUpAcorn(collision.gameObject);
            }

            else
            {
                //UI to say have your hands full with acorns, find a checkpoint to store some!
            }
            
        }

        if (collision.gameObject.tag == "EatAcornTrigger")
        {
            GM.EatExplanation();
            collision.gameObject.SetActive(false);
            Debug.Log("Reached eat trigger");
        }
    }

    public void RemainIdle()
    {
        animator.SetInteger("AnimState", 0);
        playerRB.velocity = new Vector2(0, playerRB.velocity.y);
    }


}
