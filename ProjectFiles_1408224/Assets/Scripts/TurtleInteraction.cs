using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleInteraction : MonoBehaviour
{
    public CharacterController CC;
    public bool turtleTriggered;
    public GameObject interactButton;
    public Dialogue dialogue;
    public DialogueManager DM;
    public GameObject boatTransactionUI;
    public bool activateUI = true;
    public BoatController BC;
    public GameObject turtle1;
    public GameObject turtle2;
    public GameManager GM;
    public Transform reposition;
    public GameObject notEnoughUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turtleTriggered == true && DM.isConversing == false && activateUI == true)  //CANT ACTUALLY OUT HERE WILL STAY ACTIVE EVEN WHEN NOT NEEDED
        {
            boatTransactionUI.SetActive(true);
        }

        else
        {
            boatTransactionUI.SetActive(false);
        }

        if (turtle1.transform.position.x < CC.transform.position.x)
        {
            turtle1.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else
        {
            turtle1.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void StartInteraction()
    {
        //check turtle is facing player
        
        CC.RemainIdle();
        CC.enabled = false;
        turtleTriggered = true;
        
        interactButton.SetActive(false);
        DM.StartDialogue(dialogue);
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collision detected");
            if (turtleTriggered == false)
            {
                interactButton.SetActive(true);
            }

            else
            {
                if (BC.onBoat == false)
                {
                    activateUI = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activateUI = false;
            interactButton.SetActive(false);
        }
    }

    IEnumerator NotEnough()
    {
        notEnoughUI.SetActive(true);
        yield return new WaitForSeconds(3);
        notEnoughUI.SetActive(false);
    }
    public void YesToTrade()
    {
        activateUI = false;
        
        if (GM.acorns >= 10)
        {
            GM.acorns -= 10;
           
            BC.jumpOnUI.SetActive(true);
            BC.onBoat = true;
            turtle1.transform.SetParent(BC.transform);
            turtle2.SetActive(true);
            
        }

        else
        {

            turtle2.SetActive(false);
            StartCoroutine(NotEnough());
            //Coroutine with UI saying not enough
            Debug.Log("not enough acorns, must go back and get more");
        }
    }

    public void NoToTrade()
    {
        activateUI = false;
    }
}
