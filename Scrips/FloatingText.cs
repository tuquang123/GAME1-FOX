using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 1f;

    public Vector3 Offset = new Vector3(0, 2, 0);

    public Vector3 RandomizeInsity = new Vector3(1 , 0, 0);

    void Update()
    {
        Destroy(gameObject,DestroyTime);

        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeInsity.x, RandomizeInsity.y),
        Random.Range(-RandomizeInsity.y, RandomizeInsity.y),
        Random.Range(-RandomizeInsity.z, RandomizeInsity.z));
    }
    
}
