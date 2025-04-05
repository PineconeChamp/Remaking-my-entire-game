using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public GameObject player;
    public float timeCharged;
    public float totalDamage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DataReciever(float damage, float timeCharged)
    {
        this.timeCharged = timeCharged;
        totalDamage = damage;
    }

    public void CalculateDamage()
    {
        totalDamage = totalDamage * timeCharged * 10f;
        Debug.Log("Calculated " + totalDamage + " damage");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {

            CalculateDamage();
            enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(totalDamage);

            //How damage types may be passed and calculated inside of the enemys take damages script.
            //enemyHealth.TakeDamage(physDamage, magicDamage, cursedDamage);
        }
    }
}
