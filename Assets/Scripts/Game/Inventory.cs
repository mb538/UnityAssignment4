using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Text goldCounter;

    private int gold;
    public int startGold = 250;

    private void Awake() 
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start()
    {
        gold = startGold;
        goldCounter.text = gold.ToString() + "$";
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int gold)
    {
        this.gold = gold;
        goldCounter.text = this.gold.ToString() + "$";
    }
}
