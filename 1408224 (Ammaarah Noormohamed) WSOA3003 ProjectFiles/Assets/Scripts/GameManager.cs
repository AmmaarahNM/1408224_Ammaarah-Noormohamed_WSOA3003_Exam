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
    public GameObject acornsReleased;
    public int totalAcornTally;

    public GameObject endUI;
    public GameObject stars;
    public Animator starsAnim;
    public Text endComment;
    public Text finalScore;
    public GameObject dieView;

    // Start is called before the first frame update
    void Start()
    {
        //lastCheckpoint.position = startPos.position;
        health = 3;
        CC.enabled = true;
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
        CC.RemainIdle();
        CC.enabled = false;
        dieView.SetActive(true);

        yield return new WaitForSeconds(2);
        dieView.SetActive(false);
        foreach (GameObject acorn in startAcorns)
        {
            acorn.SetActive(true);
        }
        acorns = 0;
        CC.transform.position = lastCheckpoint.position;
        CC.enabled = true;
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
        if (acorns > 0)
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

    public void ReleaseAcorns()
    {
        acornsReleased.SetActive(true);
    }

    public void EndOfGame()
    {
        CC.RemainIdle();
        CC.gameObject.SetActive(false);
        totalAcornTally = storedAcorns + acorns;
        endUI.SetActive(true);
        finalScore.text = totalAcornTally.ToString();
        //tally text = totalAcornTally
        
        if (totalAcornTally < 30)
        {
            stars.SetActive(true);
            starsAnim.Play("OneStar");
            endComment.text = "Oh no! You don't have enough acorns to progress! Try Again...";
            //one star UI
        }

        else if (totalAcornTally >= 45)
        {
            stars.SetActive(true);
            starsAnim.Play("ThreeStar");
            endComment.text = "WELL DONE! Your family will be happily feasting in the coming days!";
            //three star UI
        }

        else
        {
            
            //two star UI
            stars.SetActive(true);
            starsAnim.Play("TwoStar");
            endComment.text = "An admirable effort. Your family may have to ration, but they'll have enough food to survive";
        }

    }
}
