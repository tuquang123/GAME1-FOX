using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;

    float timetween;
    public float starttimebetween;
    
    void Start()
    {
        timetween = starttimebetween;
    }

    
    void Update()
    {
        if(timetween <= 0)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            timetween = starttimebetween;

        }
        else
        {
            timetween -= Time.deltaTime;
        }
    }
}
