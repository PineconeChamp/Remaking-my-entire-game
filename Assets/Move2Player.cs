using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;

public class Move2Player : MonoBehaviour
{

    //Moves the enemy toward the player, sends damage commands on contact, stuns and pushes the enemy after a successful attack

    [Header("Enemy Movement")]
    public float moveSpeed = 2.5f;
    public float enemyDamage = 1;
    public float distanceFromPlayer;
    public Rigidbody2D enemyRB;

    [Header("Player Reference")]
    public Transform playerPos;
    public PlayerHealth playerDamage;
    public Vector2 directionPlayer;
    public Vector2 directionPlayerInverted;

    [Header("Stun Vars")]
    [SerializeField] private bool enemyStunned;
    [SerializeField] private string stunType;
    [SerializeField] private IEnumerator StunCor;
    [SerializeField] private IEnumerator PushCD;
    [SerializeField] private bool PCD_Active;
    public float pushTime;
    public float stunForce;

    public float aoeRadius = 10f;
    public float pushForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        PCD_Active = false;
        enemyStunned = false;
        StunCor = Stun(null);
        PushCD = PushCooldown();
    }
    // Update is called once per frame
    void Update()
    {
        //Spacial Awareness. Check for nearby enemies and try circling the player instead of beelining if needed
        
        //Check if player gets to fast and resets speed to max speed
        if (enemyRB.velocity.magnitude > 1)
        {
            enemyRB.velocity = directionPlayer * moveSpeed;
        }

        if (playerPos != null)
        {
            distanceFromPlayer = Vector2.Distance(transform.position, playerPos.transform.position);
            directionPlayer = playerPos.transform.position - transform.position;
            directionPlayer.Normalize();
            directionPlayerInverted = directionPlayer * -1;
            //Debug.Log("Direction");
            //Debug.Log(direction);
            //Debug.Log("Dist From Player");
            //Debug.Log(distanceFromPlayer);
            //Debug.Log("direction * moveSpeed");
            //Debug.Log(direction * moveSpeed);

            if (enemyStunned == false)
            {
                //enemyRB.velocity = directionPlayer * moveSpeed;
                enemyRB.AddForce(directionPlayer * moveSpeed, ForceMode2D.Force);
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

    public void RecieveAOEPushback()
    {
        if (enemyStunned == false && PCD_Active == false)
        {
            enemyStunned = true;
            StartCoroutine("Stun", stunType);

            PCD_Active = true;
            StartCoroutine("PushCooldown", PCD_Active);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {
            //Debug.Log("FOUND-TAG");
            //Stuns Enemy After Attack
            stunType = "OutputDMG";
            playerDamage = collision.GetComponent<Collider2D>().GetComponent<PlayerHealth>();
            playerDamage.TakeDamage(enemyDamage);

            if (enemyStunned == false && PCD_Active == false)
            {
                enemyStunned = true;
                StartCoroutine("Stun", stunType);

                PCD_Active = true;
                StartCoroutine("PushCooldown", PCD_Active);

                PerformAOEPushback();
            }
            //Gives Player Damage Command          
        }
    }

    private IEnumerator Stun(string stunType)
    {
        //Stun logic depending on stun type
        //Debug.Log("STUN-SELF");

        if (stunType == "OutputDMG")
        {
            pushTime = 1f;
        }
        else if (stunType == "TookDamage")
        {
            pushTime = 0.1f;
        }
        else
        {
            pushTime = 0f;
        }

        float duration = pushTime;
        float time = 0;
        Vector2 startPosition = transform.position;
        Vector2 target = directionPlayerInverted;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        enemyRB.velocity = Vector2.zero;;

        yield return new WaitForSeconds(0.2f);

        enemyStunned = false;
        stunType = "";
    }

    private IEnumerator PushCooldown()
    {
        yield return new WaitForSecondsRealtime(2f);
        yield return PCD_Active = false;
    }

    public void PerformAOEPushback()
    {
        // Detect all colliders within the AOE radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);

        foreach (var hitCollider in hitColliders)
        {
            // Check if the collider belongs to an enemy with a Rigidbody2D
            if (hitCollider.CompareTag("Enemy"))
            {
                Move2Player enemyStunCoords = hitCollider.GetComponent<Move2Player>();
                if (enemyStunCoords != null)
                {

                    //Run Stun Functions
                    enemyStunCoords.RecieveAOEPushback();
                }
            }
        }
    }
}
