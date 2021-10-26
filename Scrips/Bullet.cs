using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource death;

    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        rb.velocity = transform.right * speed;
        anim = GetComponent<Animator>();
        death = GetComponent<AudioSource>();

    }
    
    public void OnBullet()
    {
        anim.SetTrigger("Death");
        death.Play();
        rb.velocity = Vector2.zero;
    }
    



    private void Death()
    {
        Destroy(this.gameObject);
    }
}
