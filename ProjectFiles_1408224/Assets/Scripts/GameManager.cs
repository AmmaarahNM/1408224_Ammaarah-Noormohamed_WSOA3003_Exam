using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int health;
    public int acorns;
    public ParticleSystem collectionParticles;
    public CameraFollow CF;
    public Text acornValue;
    public CharacterController CC;
    public Dialogue dialogue;

    public Transform lastCheckpoint;
    //public Transform startPos;
    public int totalBankedAcorns;
    public int storedAcorns;

    public Text storedAcornValue;
    public GameObject checkpointUI;
    public GameObject reachedCheckpoint;
    int acornsAtCheckpoint;
    public GameObject[]hearts = new GameObject[3];
    public GameObject[] startAcorns;

    // Start is called before the first frame update
    void Start()
    {
        //lastCheckpoint.position = startPos.position;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        acornValue.text = acorns.ToString();
        storedAcornValue.text = storedAcorns.ToString();
        foreach (GameObject heart in hearts)
        {
            if (System.Array.IndexOf(hearts, heart) >= health)
            {
                heart.SetActive(false);
            }
        }
    }

    public void LoseHealth()
    {

        if (health > 1)
        {
            health--;
            CF.ShakeTrigger();
            //make player flash red or something
            //move player backwards a few 
            //ui update
        }

        else
        {
            health = 0;
            //update UI
            CF.ShakeTrigger();
            CC.isDrowning = false;
            StartCoroutine(Reset());
            //Coroutine to delay: reset to last checkpoint, acorns = 0, health reset to full, reset notificatio UI;
            //isdrowning is false
        }

        Debug.Log("health = " + health);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject acorn in startAcorns)
        {
            acorn.SetActive(true);
        }
        acorns = 0;
        CC.transform.position = lastCheckpoint.position;
        health = 3;
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(true);
        }
    }
    public void RegenerateAcorns()
    {
        //have acorns in an array, for every array value existing when health is lost, re-activate that particualar acorn in the level (for each?)
        //empty array
    }

    public void PickUpAcorn(GameObject acorn)
    {
        acorns++;
        Debug.Log("acorns= " + acorns);
        collectionParticles.transform.position = acorn.transform.position;
        collectionParticles.Play();
        //play collection audio
        acorn.SetActive(false);
        //check which acorns - or do that in separate script - do something similar to key collection with acorn[1], etc. HAVE A SCRIPT JUST FOR ACORN VALUE CHECK
    }

    public void EatExplanation()
    {
        CC.canEatAcorns = true;
        CC.RemainIdle();
        CC.enabled = false;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void CheckpointReached()
    {
        checkpointUI.SetActive(true);
        acornsAtCheckpoint = acorns;
        CC.RemainIdle();
        CC.enabled = false;
    }
    public void IncreaseStoredValue()
    {
        if (storedAcorns < acornsAtCheckpoint)
        {
            storedAcorns++;
            acorns--;
        }
    }

    public void DecreaseStoredValue()
    {
        if (storedAcorns > 0 && acorns < 25)
        {
            storedAcorns--;
            acorns++;
        }
    }

    public void ExitCheckpoint()
    {

        totalBankedAcorns = storedAcorns;
        //storedAcorns = 0;
        checkpointUI.SetActive(false);
        reachedCheckpoint.SetActive(false);
        CC.enabled = true;
    }
}
