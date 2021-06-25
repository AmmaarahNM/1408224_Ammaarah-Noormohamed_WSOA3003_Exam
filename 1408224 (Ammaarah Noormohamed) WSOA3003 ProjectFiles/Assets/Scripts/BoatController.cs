using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public bool onBoat;
    public bool squirrelOn;
    public GameObject jumpOnUI;
    public bool atEnd;
    public CharacterController CC;
    public GameObject turtleTrigger;
    public Transform endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onBoat == true && squirrelOn == true && atEnd == false)
        {
           transform.position = Vector3.MoveTowards(transform.position, endPos.position, 4 * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, endPos.position)<0.2)
        {
            atEnd = true;
            CC.enabled = true;
            Debug.Log("at end!");
        }
    }

    IEnumerator FreezeSquirrel()
    {
        yield return new WaitForSeconds(0.1f);
        squirrelOn = true;
        CC.RemainIdle();
        CC.enabled = false;
        jumpOnUI.SetActive(false);
        turtleTrigger.SetActive(false);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            if (onBoat == true) //&& (CC.gameObject.transform.position.y >= transform.position.y +1))
            {
                StartCoroutine(FreezeSquirrel());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            squirrelOn = false;
        }
    }
}
