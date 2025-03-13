using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float maxHealth = 5f;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Took damage");

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    public void Death()
    {
        Debug.Log("Enemy Dead");
        //GameObject Gem = Instantiate([What to instantiate], [the position to instantiate]);

        //Add small random movement
        //bolt.GetComponent<Rigidbody2D>().AddForce(firePoint.up * 30, ForceMode2D.Impulse);
        Destroy(gameObject);
    }
}
