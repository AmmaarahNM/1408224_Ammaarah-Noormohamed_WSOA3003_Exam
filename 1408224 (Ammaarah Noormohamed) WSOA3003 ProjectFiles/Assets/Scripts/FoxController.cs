using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed;

    public bool isMovingToEnd = true;
    public bool isPlayerSeen = false;

    public float seenDistance; // USE RAYS SO DOESN'T PASS TREE - OTHERWISE HAVE A BOOL STATING WETHER PLAYER IS BETWEEN THOSE TREES OR NOT

    public GameObject player;
    public Animator anim;
    bool isDead;
    public GameManager GM;
    public CharacterController CC;
    //private Animator animator;
    //private AudioSource attackSound;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint.position;
        //animator = GetComponent<Animator>();
        //attackSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead == false)
        {
            if ((Vector3.Distance(transform.position, player.transform.position) < seenDistance) && player.transform.position.y < transform.position.y)
            {
                isPlayerSeen = true;
            }

            else
            {
                isPlayerSeen = false;
            }


            //Ordinary patrol - not going after player
            if (isPlayerSeen == false)
            {
                //speed is 2
                if (transform.position != startPoint.position && transform.position != endPoint.position)
                {
                    //walking animation
                }

                if (Vector3.Distance(transform.position, startPoint.position) < 0.1)
                {
                    isMovingToEnd = true;

                }

                if (Vector3.Distance(transform.position, endPoint.position) < 0.1)
                {
                    isMovingToEnd = false;
                }
            }

            else
            {
                Debug.Log("PLAYER SEEN");


                //attack sprint anim
                //increase speed

                //Make the enemy face the player and move towards her
                if (transform.position.x < player.transform.position.x)
                {
                    isMovingToEnd = true;

                }

                else
                {
                    isMovingToEnd = false;
                }

            }

            if (isMovingToEnd == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        else
        {
            anim.Play("foxDeath");
            StartCoroutine(DelayDeath());
            //start coroutine to destroy and also activate a bunch of acorns
        }
        

    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(1.2f);
        GM.ReleaseAcorns();
        yield return new WaitForSeconds(0.3f);
        //activate a bunch of acorns and the animation for them dropping - call from GM
        gameObject.SetActive(false);
    }

    public void Death()
    {
        isDead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.transform.position.y < transform.position.y)
        {
            speed = 0;
            anim.Play("foxAttack");
            //player loses health - PUT THIS IN CHARACTER CONTROLLER THOUGH
            //attackSound.Play();
            GM.LoseHealth();
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                //transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                CC.playerRB.velocity = new Vector2(-3, CC.playerRB.velocity.y);

            }

            else
            {
                //transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                CC.playerRB.velocity = new Vector2(3, CC.playerRB.velocity.y);

            }
            StartCoroutine("ResumeSpeed");
        }

    }

    private IEnumerator ResumeSpeed()
    {
        yield return new WaitForSeconds(1);
        speed = 4;
        anim.Play("foxWalk");
    }
}

