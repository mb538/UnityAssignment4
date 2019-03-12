using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private bool hasEnemy = false;
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Enemy")
        {
            hasEnemy = true;
        } 
    }
    private void OnCollisionExit(Collision collision)
    {
        hasEnemy = false;
    }

    public bool getHasEnemy()
    {
        return hasEnemy;
    }
}
