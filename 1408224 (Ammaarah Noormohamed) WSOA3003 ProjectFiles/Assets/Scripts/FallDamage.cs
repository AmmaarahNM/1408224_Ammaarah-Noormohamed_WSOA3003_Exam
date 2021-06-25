using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public GameManager GM;
    public GameObject fallDamageUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TakeFallDamage()
    {
        yield return new WaitForSeconds(0.8f);
        GM.LoseHealth();
        yield return new WaitForSeconds(1.2f);
        fallDamageUI.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fallDamageUI.SetActive(true);
            StartCoroutine(TakeFallDamage());
        }
    }
}
