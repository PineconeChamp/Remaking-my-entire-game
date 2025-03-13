using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireAngle: MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet1;
    public Transform firePoint;
    public float timeCharged;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

    }

    public void Fire()
    {
        //The whole Fire script should take in a bunch of values based off of what upgrades ect the player has optained that directly affect things about the bullet
        //Effects dont matter so much as they can take into effect say for example after an enemy is hit it runs a script that has all current upgrades and returns all the values from a public mathing function

        GameObject bolt = Instantiate(bullet1, firePoint.position, firePoint.rotation);

        bolt.GetComponent<BoltProjectile>().ChargeTimeReciever(timeCharged);
        bolt.GetComponent<Rigidbody2D>().AddForce(firePoint.up * 30, ForceMode2D.Impulse);
    }
}
