using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    GameObject thePlayer;

    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            thePlayer.GetComponent<PlayerBehavior>().Die();
        }
    }
}
