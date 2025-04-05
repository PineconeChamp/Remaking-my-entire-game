using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public float currentExp;
    public float currentLevel;
    public float levelCap;

    // Start is called before the first frame update
    void Start()
    {
        currentExp = 0;
        currentLevel = 1;
        levelCap = 10;
    }

    public void UpdateExp(float exp)
    {
        currentExp += exp;
        if (currentExp >= levelCap)
        {
            UpdateLevel();
        }
    }

    public void UpdateLevel()
    {
        currentExp += 1;

        levelCap += levelCap/1.2f;
    }
}
