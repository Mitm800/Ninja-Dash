using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 0;
    public Animator animator;
    public SpriteRenderer sprite;
    public Rigidbody2D rigidBody2D;
    GameObject thePlayer;

    private void Start() {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate() {
        rigidBody2D.velocity = new Vector3 (-speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            speed = 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
           animator.SetBool("DeadAnimation", true);
            Invoke("DisableSprite", 0.5f);
            thePlayer.GetComponent<PlayerBehavior>().Die();
        }
    }

    private void DisableSprite(){
        sprite.enabled = false;
    }
}
