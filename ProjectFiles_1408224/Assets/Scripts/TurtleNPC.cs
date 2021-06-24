using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleNPC : MonoBehaviour
{
    public Dialogue dialogue;
    bool turtleTriggered;

    public CharacterController CC;
    //public DialogueManager DM;
    public GameObject npc1Buttons;
    public GameObject interactButton;
    public GameManager GM;
    public bool isMovingToEnd;
    public GameObject boatTransactionUI;
    public GameObject jumpOnBoat;
    public bool onBoat;
    public GameObject boat;
    public GameObject turtle;
    public Transform endPos;
    public bool boatMoving;

    // Start is called before the first frame update
    void Start()
    {
        isMovingToEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (turtleTriggered == true && CC.enabled == true && onBoat)
        //{
        //    boatTransactionUI.SetActive(true);
        //}

        if (onBoat == true)
        {
            turtle.transform.position = new Vector2(boat.transform.position.x, boat.transform.position.y + 1.54f);
            boatTransactionUI.SetActive(false);
        }

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (turtleTriggered == false)
            {
                npc1Buttons.SetActive(true);
                interactButton.SetActive(true);

            }

            else
            {
                if (onBoat == false)
                {
                    boatTransactionUI.SetActive(true);
                }

                else
                {
                    
                    boatTransactionUI.SetActive(false);
                    jumpOnBoat.SetActive(false);
                    BoatMoves();
                }

            }

        }

        if (collision.gameObject.tag == "BoatPos")
        {
            CC.enabled = true;
            boatMoving = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //startInteraction = false;
            //deactivate last one?
            npc1Buttons.SetActive(false);
            boatTransactionUI.SetActive(false);

            if (onBoat == true && Vector2.Distance(boat.transform.position, endPos.position) <= 0.1)
            {
                onBoat = false;
                CC.enabled = true;
                
                if (isMovingToEnd==true)
                {
                    isMovingToEnd = false;
                }
                else
                {
                    isMovingToEnd = true;
                }
            }
            


        }
    }

    public void StartInteraction()
    {
        
        //checkpoint1.SetActive(true);
        CC.RemainIdle();
        CC.enabled = false;
        turtleTriggered = true;
        //deactivate cc (but first set to idle)
        interactButton.SetActive(false);
        //dialogueBox.SetActive(true);
        //activation squirrel text and button to continue
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        boatTransactionUI.SetActive(true);

    }

    public void YesToTrade()
    {
        if (GM.acorns >= 10)
        {
            GM.acorns -= 10;
            onBoat = true;
            if (isMovingToEnd)
            {
                turtle.transform.eulerAngles = new Vector3(0, 180, 0);
                turtle.transform.position = new Vector2(boat.transform.position.x, boat.transform.position.y + 1.54f);
            }
            //else 000
            boatTransactionUI.SetActive(false);
            jumpOnBoat.SetActive(true);

            //move turtle to boat (use boat transform minus xx so turtle always lands on boat)
            //if isMovingToEnd = true, change turtle transform otherwise make it 000
            //onBoat = true;
            //deactivate UI
            
            //activate get on boat UI
        }
        else
        {
            Debug.Log("not enough acorns!");
            //not enough acorns go search for more ui, then coroutine to deactivate it
            //MAYBE HAVE SECRET ACORNS THAT GET ACTIVATED WHEN THIS HAPPENS, HAVE TO GO BACK AND FIND THEM - ONLY ENOUGH TO PASS THOUGH
        }

    }

    public void NoToTrade()
    {
        boatTransactionUI.SetActive(false);
    }

    public void BoatMoves()
    {
        Debug.Log("Boat moving");
        
        boatTransactionUI.SetActive(false);
        jumpOnBoat.SetActive(false);
        boatMoving = true;

        //adjust squirrel position

        
        //remain idle and deactivate CC
        //if movingtoend then boat. transform.position moves towards end pos
    }
}
