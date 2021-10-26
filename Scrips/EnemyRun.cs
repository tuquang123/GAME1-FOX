using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun : StateMachineBehaviour
{
    
    public float speed = 2.5f;
    

    Transform player;
    Rigidbody2D rb;
    Boss boss;

    // input gan cac valiable
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        // find position player in x 
        Vector2 target = new Vector2(player.position.x, rb.position.y);

        // find and direction -> target with speed * time 
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

       
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
