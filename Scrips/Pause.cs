using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject Ui;

    public void Pausegame()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;

    }
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        Destroy(this.Ui);
    }
    
}
