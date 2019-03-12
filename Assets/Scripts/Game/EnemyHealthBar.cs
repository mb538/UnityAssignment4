using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Camera cam;
    public EnemyController ec;
    public Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
        float fillAmount = (float)ec.GetCurHealth() / ec.GetMaxHealth();
        healthbar.fillAmount = fillAmount;
    }
}
