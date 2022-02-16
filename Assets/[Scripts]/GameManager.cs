using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerBehavior player;
    private void Update() {
       GameObject playerGo= GameObject.FindGameObjectWithTag("Player");
        if(playerGo)
        {
            player = GetComponent<PlayerBehavior>();
        }

    }

    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

}
