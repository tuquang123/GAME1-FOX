using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele : MonoBehaviour
{
    //Object điểm đến 
    [SerializeField] private Transform destination;


    public Transform GetDestion()
    {
        return destination;
    }
}
