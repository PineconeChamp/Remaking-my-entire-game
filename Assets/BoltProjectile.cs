using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltProjectile : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public float directDamage;
    public float timeCharged;
    // Start is called before the first frame update
    void Start()
    {
        directDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChargeTimeReciever(float timeCharged)
    {
        this.timeCharged = timeCharged;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            enemyHealth = collision.collider.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(directDamage);

            //How damage types may be passed and calculated inside of the enemys take damages script.
            //enemyHealth.TakeDamage(physDamage, magicDamage, cursedDamage);
        }
    }
}
