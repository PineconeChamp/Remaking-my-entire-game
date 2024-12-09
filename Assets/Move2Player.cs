using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;

public class Move2Player : MonoBehaviour
{

    public float moveSpeed = 5;
    public Transform playerPos;
    public float distanceFromPlayer;
    public bool enemyStunned;
    public Rigidbody2D enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        enemyStunned = false;

    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, playerPos.transform.position);
        Vector2 direction = playerPos.transform.position - transform.position;
        direction.Normalize();
        Debug.Log("Direction");
        Debug.Log(direction);
        Debug.Log("Dist From Player");
        Debug.Log(distanceFromPlayer);
        Debug.Log("direction * moveSpeed");
        Debug.Log(direction * moveSpeed);

        if (distanceFromPlayer >= 2 && enemyStunned == false) 
        {
            enemyRB.velocity = direction * moveSpeed;
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }
}
