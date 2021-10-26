using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chat : MonoBehaviour
{
    public GameObject start;
    
    private void Star()
    {
        start = GetComponent<GameObject>();
        start.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            start.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            start.SetActive(false);

        }
    }
}
