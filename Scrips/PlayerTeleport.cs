using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    //vi tri dich chuyen
    private GameObject currentTeleporter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleport>().GetDestion().position;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tele"))
        {
            currentTeleporter = collision.gameObject;
        }  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tele"))
        {
            currentTeleporter = null;
        }
    }
}
