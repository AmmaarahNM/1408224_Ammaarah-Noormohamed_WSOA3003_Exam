using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    bool playerSeen = false;

    public Vector3 offset;

    public float shakeDuration = 0;
    public float shakeIntensity = 0.3f;


    void Start()
    {
        offset = transform.position - player.transform.position;
    }


    void Update()
    {
        if (shakeDuration == 0)
        {
            if (player != null)
            {
                playerSeen = true;
            }

            if (playerSeen == true)
            {
                transform.position = player.transform.position + offset;
            }
        }

        if (shakeDuration > 0)
        {

            transform.localPosition += Random.insideUnitSphere * shakeIntensity;
            StartCoroutine("StopShaking");
        }


    }
    // Creating function to call somewhere else to trigger camera shake
    public void ShakeTrigger()
    {
        shakeDuration = 1;
    }

    //Stop shake  after a certain time and reset position
    IEnumerator StopShaking()
    {
        yield return new WaitForSeconds(0.5f);
        shakeDuration = 0;
        transform.position = player.transform.position + offset;

    }
}
