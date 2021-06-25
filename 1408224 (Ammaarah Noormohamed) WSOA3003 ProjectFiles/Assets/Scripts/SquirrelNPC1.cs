using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelNPC1 : MonoBehaviour
{
    bool squirrelOneTriggered;
    public CharacterController CC;
    public GameObject npc1Buttons;
    public GameObject interactButton;
    public GameObject checkpoint1;
    //public GameObject dialogueBox;

    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //destroy trigger once full interaction has occured???

        //if squirrelOneTriggered == true then activate checkpoint there
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (squirrelOneTriggered == false)
            {
                npc1Buttons.SetActive(true);
                interactButton.SetActive(true);
                
            }
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //startInteraction = false;
            //deactivate last one?
            npc1Buttons.SetActive(false);
            
        }
    }

    public void StartInteraction()
    {
        squirrelOneTriggered = true;
        checkpoint1.SetActive(true);
        CC.RemainIdle();
        CC.enabled = false;
        //deactivate cc (but first set to idle)
        interactButton.SetActive(false);
        //dialogueBox.SetActive(true);
        //activation squirrel text and button to continue
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }
}
