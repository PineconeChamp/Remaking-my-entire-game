using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{


    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {

        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        Vector2 normalSpeed = new Vector2(speedX, speedY);
        normalSpeed.Normalize();
        normalSpeed *= moveSpeed;
        rb.velocity = normalSpeed;
    }
}
