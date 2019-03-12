using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 15f;
    public int bulletDamage = 2;
    public float countdown = 2f;

    [Header("Unity Settings")]
    public Transform turretHead;
    public Transform[] firePoints;
    public GameObject nextUpgrade;
    public GameObject shootEffect;

    private Transform target;

    void Start()
    {
        InvokeRepeating("SelectTarget", 0f, 0.5f);
    }

    void SelectTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }
        if(closestEnemy != null && shortestDistance <= range)
        {
            target = closestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        turretHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Shoot(); 
    }

    void Shoot()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            Transform fp = firePoints[Random.Range(0, firePoints.Length)];
            GameObject bulletGO = ObjectPooler.instance.GetObjectFromPool();
            bulletGO.transform.position = fp.position;
            bulletGO.transform.rotation = fp.rotation;
            bulletGO.SetActive(true);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetTarget(target);
                bullet.damage = bulletDamage;
            }
            GameObject effect = (GameObject)Instantiate(shootEffect, fp.position, fp.rotation);
            Destroy(effect, 3f);
            countdown = 2f;
        }
    }
}
