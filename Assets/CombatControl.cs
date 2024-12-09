using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{
    public bool readyToShoot;
    public FireAngle fireScript;

    //Just a placeholder for adding a selection of different spells
    private float bulletType1 = 1;

    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyToShoot == true)
        {
            fireScript.Fire();
        }
    }
}
