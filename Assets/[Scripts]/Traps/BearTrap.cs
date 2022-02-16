using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    private Animator trap_animator;
    GameObject thePlayer;

    void Start()
    {
        trap_animator = gameObject.GetComponent<Animator>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            trap_animator.SetBool("Activate", true);
            thePlayer.GetComponent<PlayerBehavior>().Die();
        }
    }
}
