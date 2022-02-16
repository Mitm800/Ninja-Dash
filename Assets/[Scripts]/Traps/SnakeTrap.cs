using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SnakeTrap : MonoBehaviour
{
    private Animator trap_animator;
    GameObject thePlayer;
    public SpriteRenderer sprite;

    void Start()
    {
        trap_animator = gameObject.GetComponent<Animator>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            sprite.enabled = true;
            trap_animator.SetBool("Idle", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            trap_animator.SetBool("Attack", true);
            thePlayer.GetComponent<PlayerBehavior>().Die();
        }
    }
}
