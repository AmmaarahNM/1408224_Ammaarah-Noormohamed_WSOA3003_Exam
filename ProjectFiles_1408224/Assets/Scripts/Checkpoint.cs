using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public GameManager GM;
    //public Transform resetPoint;
    //public CharacterController CC;
    
 

    bool thisOneVisited;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            GM.CheckpointReached();
            

            if (thisOneVisited == false)
            {
                GM.lastCheckpoint.position = collision.gameObject.transform.position;
                GM.reachedCheckpoint.SetActive(true);
          
                thisOneVisited = true;
                Debug.Log("new checkpoint reached");
            }
            

        }
    }

    
}
