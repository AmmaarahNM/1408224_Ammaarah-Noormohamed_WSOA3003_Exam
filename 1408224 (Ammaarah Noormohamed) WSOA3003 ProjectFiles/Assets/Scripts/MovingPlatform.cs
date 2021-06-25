using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed;
    public bool isMovingToEnd;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = start.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == start.position)
        {
            isMovingToEnd = true;
        }

        if (transform.position == end.position)
        {
            isMovingToEnd = false;
        }

        if (isMovingToEnd == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, start.position, speed * Time.deltaTime);
        }
    }

}
