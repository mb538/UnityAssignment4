using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public GameObject impactEffect;

    public int damage = 2;

    void Start()
    {
        Invoke("DestroyBullet", 2f); // Destroys the bullet after 2 seconds
    }

    void Update()
    {
        if(target == null)
        {
            DestroyBullet();
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.3f);
        }
       
        if(transform.position == target.position)
        {
            TargetHit();
            return;
        }
    }

    private void TargetHit()
    {
        EnemyController ec = target.gameObject.GetComponent<EnemyController>();
        ec.SetCurHealth(ec.GetCurHealth() - damage);
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 3f);
        DestroyBullet();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false); 
    }
}
