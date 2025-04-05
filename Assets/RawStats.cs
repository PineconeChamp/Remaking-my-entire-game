using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RawStats : MonoBehaviour
{
    //Contains a predefined list of stats and takes in the values of any permanent upgrades the player has unlocked.
    //Stats can be sent to the weapon firing script that can then interact with any runes that may have been collected and added to the stave.

    public CombatControl combatControl;
    public PlayerHealth healthControl;
    public FireAngle FireAngle;


    public float shotCooldownTime;
    public float reloadTime;
    public float maxAmmo;
    public float runeMaxCharge;
    public float critRate;
    public float critDamage;
    public float shotSpeed;
    public float maxHealth;

    private void Start()
    {
        //Load in stats from character picker
        shotCooldownTime = 0.2f;
        maxAmmo = 10f;
        reloadTime = 0.5f;
        runeMaxCharge = 0f;
        //GetCombatStats();
    }

    private void GetCombatStats()
    {
        combatControl.ReceiveValues(shotCooldownTime, maxAmmo, reloadTime);
    }

    public void GetHealthStats()
    {
        healthControl.UpgradeHealth(maxHealth);
    }
}
