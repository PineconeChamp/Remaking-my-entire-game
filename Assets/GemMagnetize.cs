using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GemMagnetize : MonoBehaviour
{
    public float magenetizeForce = 0.5f;
    public bool CanMagnetize;
    public GameObject player;
    public Vector2 playerDir;
    public Rigidbody2D rb;
    public float orbValue = 1.2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("I exist");
        CanMagnetize = false;
    }
    void Update()
    {
        playerDir = player.transform.position - transform.position;
        playerDir.Normalize();

        if (rb.velocity.magnitude > 2)
        {
            rb.velocity = playerDir * magenetizeForce;
        }
        if (CanMagnetize)
        {
            rb.AddForce(playerDir * magenetizeForce, ForceMode2D.Force);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        //directionPlayerInverted = directionPlayer * -1;
    }

    public void InitiateMagnetize()
    {
        CanMagnetize = !CanMagnetize;
        Debug.Log("Can Magnetize Equals: " + CanMagnetize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnet"))
        {
            InitiateMagnetize();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            //Unimplemented

            player.GetComponent<PlayerLevel>().UpdateExp(orbValue);

            //Play sounds or something

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnet"))
        {
            InitiateMagnetize();
        }
    }
}
