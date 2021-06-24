using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public TurtleNPC TN;
    public CharacterController CC;
    public Transform endPos;
    bool squirrelOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TN.boatMoving == true && squirrelOn == true)
        {
            CC.RemainIdle();
            CC.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, 3 * Time.deltaTime);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            squirrelOn = true;
        }
        
    }
}
