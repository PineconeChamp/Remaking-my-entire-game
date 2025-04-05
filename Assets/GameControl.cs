using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    public bool playerAlive;
    public GameObject player;
    //This script should spawn enemies, increase dificulty, activate boss stages, and activate win conditions
    //The script should take in difficulty values predefined before a game begins, a bit like the darkness system in 20 mins till dawn

    private void Start()
    {
        playerAlive = true;
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerDataPreset myObject = new PlayerDataPreset();
        myObject.testInt = 1;
        myObject.testFloat = 47.5f;
        myObject.testString = "Dr Charles Francis";
        string json = JsonUtility.ToJson(myObject);
    }

    public void SpawnerLoop()
    {
        while (playerAlive)
        {

        }
    }

    public void PlayerDeathUpdate()
    {
        playerAlive = false;
    }

    public void UpdateDifficulty()
    {
        
    }

    public void EndGame()
    {

    }
}
