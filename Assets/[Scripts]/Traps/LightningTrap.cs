using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : MonoBehaviour
{
    private Animator lightning_animator;
    GameObject thePlayer;
    public SpriteRenderer sprite;
    public CapsuleCollider2D collider;

    void Start()
    {
        lightning_animator = gameObject.GetComponent<Animator>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            lightning_animator.SetBool("Activate", true);
            Invoke("DisableSprite", 0.5f);
        } else if(!sprite.enabled){
            collider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(sprite.enabled && other.gameObject.CompareTag("Player")){
            lightning_animator.SetBool("Activate", true);
            thePlayer.GetComponent<PlayerBehavior>().Die();
        }
    }

    private void DisableSprite(){
        sprite.enabled = false;
    }
}
