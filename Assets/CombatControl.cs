using JetBrains.Annotations;
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
    [SerializeField] private float runeMaxCharge;

    [SerializeField] private float totalDamage;

    private float timeCharged;
    private float startChargeTime;
    private float elapsedChargeTime;

    public FireAngle fireScript;
    public bool finishedSetup;

    private bool releasedButton = false;

    // Start is called before the first frame update
    void Start()
    {
        finishedSetup = false;
        shotCooldownTime = 0.1f;
        maxAmmo = 5;
        ammoCount = maxAmmo;
        reloadingGun = false;
        reloadTime = 0.5f;
        readyToShoot = true;
        totalDamage = 2f;
        timeCharged = 0;
    }

    public void ReceiveValues(float shotCooldownTime, float maxAmmo, float reloadTime)
    {
        //Should get run by a weapon choice script at the start of the game
        this.shotCooldownTime = shotCooldownTime;
        this.maxAmmo = maxAmmo;
        this.reloadTime = reloadTime;
        finishedSetup = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HandleShot());
        }
    }

    private IEnumerator HandleShot()
    {
        releasedButton = false;
        timeCharged = 0f;
        float startChargeTime = Time.time;

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0) && readyToShoot);

        releasedButton = true;
        float elapsedChargeTime = Time.time - startChargeTime;
        timeCharged = elapsedChargeTime;

        //Debug.Log("Time Charged: " + timeCharged);

        if (ammoCount >= 1)
        {
            ammoCount--;
            readyToShoot = false;
            fireScript.Fire(totalDamage, timeCharged);
            timeCharged = 0;

            StartCoroutine("ShotCooldown", readyToShoot);

        }
        else
        {
            reloadingGun = true;
            StartCoroutine("ReloadTime", reloadingGun);
        }
    }


    private IEnumerator WaitForLift()
    {
        startChargeTime = Time.time;

        yield return new WaitUntil(() => releasedButton == true);

        elapsedChargeTime = Time.time - startChargeTime;
        timeCharged = elapsedChargeTime;
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
