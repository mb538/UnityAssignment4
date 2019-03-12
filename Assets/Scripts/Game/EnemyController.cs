using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]
    public int maxHealth = 10;
    private int curHealth = 10;
    public int goldAmount = 20;

    [Header("Unity Settings")]
    public NavMeshAgent agent;
    public GameObject defeatedEffect;

    private Transform destination;
    private bool isAlive = true;

    void Start()
    {
        curHealth = maxHealth;
        agent.SetDestination(destination.position);
        InvokeRepeating("FindPath", 1f, 0.5f);
    }
    
    private void FindPath()
    {
        agent.SetDestination(destination.position);
        if(agent.remainingDistance <= 2f)
        {
            GameController.instance.curHealth--;
            Destroy(this.gameObject);
        }
    }
    
    private void Update()
    {
        if (curHealth <= 0 && isAlive == true)
        {
            EnemyDefeated();
        }
    }
    
    public void EnemyDefeated()
    {
        isAlive = false;
        Inventory.instance.SetGold(Inventory.instance.GetGold() + goldAmount);
        GameObject effect = (GameObject)Instantiate(defeatedEffect, transform.position, transform.rotation);
        Destroy(effect, 3f);
        Destroy(this.gameObject);
    }
    
    public void SetDestination(Transform _destination)
    {
        destination = _destination;
    }

    public int GetCurHealth()
    {
        return curHealth;
    }

    public void SetCurHealth(int curHealth)
    {
        this.curHealth = curHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
