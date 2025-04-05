using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    //Gives the player the ability to take damage, handles IFrames, and activates any movement that may be enacted on the player after being damaged

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float IFrameTime;
    [SerializeField] private float overHeal;
    public RawStats Stats;
    public PlayerControl pControl;
    private bool iFramesActive;
    private IEnumerator IFramesCo;

    // Start is called before the first frame update
    void Start()
    {
        iFramesActive = false;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        IFrameTime = 1f;
        IFramesCo = IFrames();
    }

    public void TakeDamage(float damageScale)
    {

        //Debug.Log("DMG-TAKE");
        if (iFramesActive == false)
        {
            currentHealth -= damageScale;
            if (currentHealth == 0f)
            {
                KillPlayer();
            }
            iFramesActive = true;
            //Debug.Log("RUN-ROUTINE");
            StartCoroutine(IFramesCo);
            pControl.HitEffects();
        }
    }

    public void Heal()
    {

    }

    public void UpgradeHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }


    public IEnumerator IFrames()
    {
        //Debug.Log("IFRAMES");
        yield return new WaitForSeconds(IFrameTime);
        iFramesActive = false;
    }

    public void KillPlayer()
    {

        //Should check if player can revive and if not then run player death logic
        //Dont destroy game object incase we need to add more logic later
        Debug.Log("Death Logic Runs");
    }
}
