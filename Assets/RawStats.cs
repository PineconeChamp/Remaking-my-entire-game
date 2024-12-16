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

    Dictionary<string, int> playerStatsDict = new Dictionary<string, int>()
    {
        {"damage", 10},
        {"health", 5},
        {"speed", 5}
    };

    Dictionary<string, float> weaponStats = new Dictionary<string, float>()
    {
        {"shotCD", 0},
        {"reloadTime", 0}
    };

    private void Start()
    {



        //Pull in perma upgrade values
        //Push base values to necesarry scripts

        weaponStats["shotCD"] = 0.1f;

        combatControl.ReceiveValues();
    }

    private void CheckUserChoices()
    {
        weaponStats["shotCD"] = 0.1f;

        combatControl.ReceiveValues();
    }
}
