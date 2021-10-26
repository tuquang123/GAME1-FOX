using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ResetScene());   
        }
    }
    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(4);
        PermanentUI.perm.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
