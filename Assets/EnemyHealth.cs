using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float maxHealth = 5f;
    float currentHealth;
    public GameObject Gem;
    GameObject bolt;
    public GameObject DamageText;
    private DamageTextLife dmgTXTSRC;

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

        currentHealth -= damage;

        GameObject dmgTXT = Instantiate(DamageText, transform.position, transform.rotation);
        dmgTXTSRC = dmgTXT.GetComponent<DamageTextLife>();
        dmgTXTSRC.StartLife(damage);


        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    public void Death()
    {
        Debug.Log("Enemy Dead");
        GameObject bolt = Instantiate(Gem, transform.position, transform.rotation);

        //Add small random movement
        //bolt.GetComponent<Rigidbody2D>().AddForce(firePoint.up * 30, ForceMode2D.Impulse);
        Destroy(gameObject);
    }
}
