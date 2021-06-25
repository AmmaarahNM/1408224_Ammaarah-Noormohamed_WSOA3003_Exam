using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public FoxController FC;
    public Transform start;
    bool freezePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ResetBoulder()
    {
        yield return new WaitForSeconds(0.7f);
        transform.position = start.position;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            StartCoroutine(ResetBoulder());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BoulderTrigger")
        {
            freezePos = true;
            FC.Death();
            
        }

        
        else
        {
            if ((transform.position.y < start.position.y) && freezePos == false)
            {
                StartCoroutine(ResetBoulder());
            }
        }
    }
}
