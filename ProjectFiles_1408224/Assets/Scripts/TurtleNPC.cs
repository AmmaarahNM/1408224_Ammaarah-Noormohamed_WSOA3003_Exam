using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleNPC : MonoBehaviour
{
    public bool turtleParented;
    public GameObject boat;
    private void Update()
    {
        if (turtleParented == true)
        {
            transform.SetParent(boat.transform);
        }
    }
}
