using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamageTextLife : MonoBehaviour
{
    private float lifetime = 0.5f;
    public TMP_Text tmp;
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
        gameObject.GetComponent<Canvas>().worldCamera = cam;
    }

    public void StartLife(float damage)
    {
        Debug.Log("Recieved " + damage + " damage");
        damage = MathF.Round(damage);
        Debug.Log("Rounded to " + damage + " damage");
        tmp.text = damage.ToString();
        StartCoroutine("Countdown");
    }

    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
