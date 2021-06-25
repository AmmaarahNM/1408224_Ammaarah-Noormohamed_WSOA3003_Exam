using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningExplanation : MonoBehaviour
{
    public Dialogue dialogue;
    public CharacterController CC;
    int tutorial;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("tutorial") == false)
        {
            StartCoroutine(StartGame());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.4f);
        CC.enabled = false;
        PlayerPrefs.SetInt("tutorial", 1);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
