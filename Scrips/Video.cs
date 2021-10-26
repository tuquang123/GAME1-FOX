using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    

    public int Timed;

    void start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            videoPlayer.Play();
            Destroy(gameObject, Timed);
           
        }
    }
}
