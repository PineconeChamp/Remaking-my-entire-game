using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //Moves the player using inputs, handles player physics and game feel, and recieves stun commands from the PlayerHealth script

    [Header("Movement Logic")]
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    [Header("Stun Logic")]
    public bool isStunned;
    private IEnumerator stunnedE;
    [SerializeField] private float stunTime;

    // Start is called before the first frame update
    void Start()
    {
        stunTime = 0.05f;
        rb = GetComponent<Rigidbody2D> ();
        stunnedE = Stun();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        Vector2 normalSpeed = new Vector2(speedX, speedY);
        normalSpeed.Normalize();
        normalSpeed *= moveSpeed;

        if (isStunned == false)
        {
            rb.velocity = normalSpeed;
        }
        else
        {
            //Stun handled by IEnumerator
        }
    }

    public void HitEffects()
    {
        if (isStunned == false)
        {
            isStunned = true;
            StartCoroutine("Stun", isStunned);
            return;
        }
        else if(isStunned == true)
        {
            Debug.Log("Already Stunned");
        }
        else
        {
            Debug.Log("DODGED-MULTI-STUN");
        }
    }

    private IEnumerator Stun()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        yield return isStunned = false;
    }
}
