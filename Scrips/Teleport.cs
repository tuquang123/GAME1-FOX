using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //diem den 
    [SerializeField] private Transform destination;

    public Transform GetDestion()
    {
        return destination;
    }
}
