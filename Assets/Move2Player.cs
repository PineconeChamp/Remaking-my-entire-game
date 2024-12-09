using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;

public class Move2Player : MonoBehaviour
{
    [Header("Enemy Movement")]
    public float moveSpeed = 2.5f;
    public float enemyDamage = 1;
    public float distanceFromPlayer;
    public Rigidbody2D enemyRB;

    [Header("Player Reference")]
    public Transform playerPos;
    public PlayerHealth playerDamage;

    [Header("Stun Vars")]
    [SerializeField] private bool enemyStunned;
    [SerializeField] private string stunType;
    [SerializeField] private IEnumerator StunCor;
    public float stunTime;
    public float stunForce;

    // Start is called before the first frame update
    void Start()
    {
        enemyStunned = false;
        StunCor = Stun(null);
    }
    // Update is called once per frame
    void Update()
    {
        if (playerPos != null)
        {
            distanceFromPlayer = Vector2.Distance(transform.position, playerPos.transform.position);
            Vector2 direction = playerPos.transform.position - transform.position;
            direction.Normalize();
            //Debug.Log("Direction");
            //Debug.Log(direction);
            //Debug.Log("Dist From Player");
            //Debug.Log(distanceFromPlayer);
            //Debug.Log("direction * moveSpeed");
            //Debug.Log(direction * moveSpeed);

            if (distanceFromPlayer >= 2 && enemyStunned == false)
            {
                enemyRB.velocity = direction * moveSpeed;
            }
            else if (enemyStunned == false)
            {
                enemyRB.velocity = direction * moveSpeed / 1.5f;
            }
            else
            {
                //Enemy was stunned in IEnumerator
            }
        }
        else
        {
            Debug.Log("PLAYER-EMPTY");
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("COL");
        if (collision.collider.CompareTag("Player"))
        {
            //Debug.Log("FOUND-TAG");
            //Stuns Enemy After Attack
            stunType = "OutputDMG";
            if (enemyStunned == false)
            {
                enemyStunned = true;
                StartCoroutine("Stun", stunType);

            }

            //Gives Player Damage Command
            playerDamage = collision.collider.GetComponent<PlayerHealth>();
            playerDamage.TakeDamage(enemyDamage);
        }
    }

    private IEnumerator Stun(string stunType)
    {
        //Stun logic depending on stun type
        //Debug.Log("STUN-SELF");

        if (stunType == "OutputDMG")
        {
            stunTime = 0.7f;
            stunForce = 300f;
        }
        else if (stunType == "TookDamage")
        {
            stunTime = 0.1f;
            stunForce = 300f / 3;
        }
        else
        {
            Debug.Log("FATAL-NO-STUN-TYPE");
            stunTime = 0f;
        }


        //Same as the direction vector for enemy movement
        Vector2 direction = playerPos.transform.position - transform.position;
        direction.Normalize();
        //Inverts the vector defined above
        Vector2 directionInverted = direction * -1;
        enemyRB.AddForce(directionInverted * stunForce, ForceMode2D.Force);
        Debug.Log(directionInverted * stunForce);

        enemyRB.velocity = Vector2.zero;

        yield return new WaitForSeconds(stunTime);

        enemyStunned = false;
        stunType = "";
    }
}
