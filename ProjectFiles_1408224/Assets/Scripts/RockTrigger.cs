using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public CharacterController CC;
    public Dialogue dialogue;
    bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isTriggered == false)
        {
            isTriggered = true;
            CC.RemainIdle();
            CC.enabled = false;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
