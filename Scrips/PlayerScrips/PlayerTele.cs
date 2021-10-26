using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTele : MonoBehaviour
{
    // Dịch chuyển hiện tại 
    private GameObject currentTeleporter;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Tele>().GetDestion().position;
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
