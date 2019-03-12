using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildController : MonoBehaviour
{
    public static BuildController instance;

    [Header("Unity Settings")]
    public Camera cam;
    public GameObject standardTurret;
    public Button sellButton;
    public Button buildButton;

    [Header("Turret Cost")]
    public int standardTurretCost = 100;
    public int upgradeCost = 100;

    private bool sellMode = false;

    private void Awake() 
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    private void Start()
    {
        DisableSellMode();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Node" && sellMode == false)
                {
                    if (CanAfford(standardTurretCost) == true)
                    {
                        BuildStandardTurret(hit);
                    }
                }
                if (hit.collider.gameObject.tag == "Turret" && sellMode == true)
                {
                    SellTurret(hit);
                }
                if (hit.collider.gameObject.tag == "Turret" && sellMode == false)
                {
                    if (CanAfford(upgradeCost) == true)
                    {
                        UpgradeTurret(hit);                 
                    }
                }
            }
        }
    }

    public void BuildStandardTurret(RaycastHit hit)
    {
        Inventory.instance.SetGold(Inventory.instance.GetGold() - standardTurretCost);
        Instantiate(standardTurret, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
    }

    public void SellTurret(RaycastHit hit)
    {
        Inventory.instance.SetGold(Inventory.instance.GetGold() + standardTurretCost - 20);
        Destroy(hit.collider.gameObject);
    }

    public void UpgradeTurret(RaycastHit hit)
    {
        Inventory.instance.SetGold(Inventory.instance.GetGold() - upgradeCost);
        GameObject nextUpgrade = hit.collider.GetComponent<TurretController>().nextUpgrade;
        Instantiate(nextUpgrade, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
        Destroy(hit.collider.gameObject);
    }

    public bool CanAfford(int cost)
    {
        if(Inventory.instance.GetGold() - cost >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EnableSellMode()
    {
        sellMode = true;
        sellButton.gameObject.SetActive(false);
        buildButton.gameObject.SetActive(true);
    }
    public void DisableSellMode()
    {
        sellMode = false;
        sellButton.gameObject.SetActive(true);
        buildButton.gameObject.SetActive(false);
    }
}
