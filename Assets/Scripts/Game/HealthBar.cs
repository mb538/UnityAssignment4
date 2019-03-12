using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameController gc;
    public Sprite[] healthBarArray;
    
    void Start()
    {
        GetComponent<Image>().sprite = healthBarArray[gc.curHealth];
    }

    void Update()
    {
        if (gc.curHealth >= 0 && gc.curHealth <= gc.maxHealth)
        {
            GetComponent<Image>().sprite = healthBarArray[gc.curHealth];
        }
    }
}
