using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    GameObject thePlayer;

    private void Start() {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            thePlayer.GetComponent<PlayerBehavior>().Win();
        }
    }
}
