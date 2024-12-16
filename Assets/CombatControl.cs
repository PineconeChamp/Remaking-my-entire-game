using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{

    //Allows the player to send out 'Bullets' aka spells.
    //Should check the runes that have been collected so that it can properly send out effects,
    //should take in data from RawStats damage.
    //nvm do al of this in BoltProjectile.cs



    //Just a placeholder for adding a selection of different spells
    [SerializeField] private float shotCooldownTime;
    [SerializeField] private float ammoCount;
    [SerializeField] private bool readyToShoot;
    [SerializeField] private bool reloadingGun;
    [SerializeField] private float reloadTime;
    [SerializeField] private float maxAmmo;

    public FireAngle fireScript;

    // Start is called before the first frame update
    void Start()
    {
        shotCooldownTime = 0.1f;
        maxAmmo = 5;
        ammoCount = maxAmmo;
        reloadingGun = false;
        reloadTime = 0.5f;
    }

    public void ReceiveValues()
    {
        //Should get run by a weapon choice script at the start of the game
        shotCooldownTime = 10f;
        maxAmmo = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyToShoot == true)
        {
            if (ammoCount >= 1)
            {
                ammoCount--;
                readyToShoot = false;
                fireScript.Fire();
                StartCoroutine("ShotCooldown", readyToShoot);
            }
            else
            {
                reloadingGun = true;
                StartCoroutine("ReloadTime", reloadingGun);
            }
        }
    }
    private IEnumerator ShotCooldown()
    {
        yield return new WaitForSecondsRealtime(shotCooldownTime);
        yield return readyToShoot = true;
    }

    private IEnumerator ReloadTime()
    {
        yield return new WaitForSecondsRealtime(reloadTime);
        yield return reloadingGun = false; ammoCount = maxAmmo;
    }
}
