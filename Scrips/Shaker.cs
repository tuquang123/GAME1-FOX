using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    public Animator Cam;
    
    public void CamShake()
    {
        Cam.SetTrigger("Shaker");
    }
}
