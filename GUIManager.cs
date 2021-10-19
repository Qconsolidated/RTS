using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public WorldObject primary;

    public CanvasGroup UnitPanel, BuildingPanel;
  
    public Text nameDisp;
    public Text attackDisp;
    public Text defenseDisp;
    public Text killDisp;
   
    public Slider energyBar;
    public Text energyDisp;

    public Slider healthBar;
    public Text healthDisp;


    public Text mineralDisp;
    public Text gasDisp;
    public Text populationDisp;
    //TO DO: Add Player Details to the HUD
    // Start is called before the first frame update
    void Start()
    {
        UnitPanel = GameObject.Find("UnitPanel").GetComponent<CanvasGroup>();
        BuildingPanel = GameObject.Find("BuildingPanel").GetComponent<CanvasGroup>();
        energyBar = GameObject.Find("UnitEnergyBar").GetComponent<Slider>();
        healthBar = GameObject.Find("UnitHealthBar").GetComponent<Slider>();
        nameDisp = GameObject.Find("UnitName").GetComponent<Text>();
        healthDisp = GameObject.Find("HealthDisp").GetComponent<Text>();
        attackDisp = GameObject.Find("AttackDisp").GetComponent<Text>();
        defenseDisp = GameObject.Find("DefenseDisp").GetComponent<Text>();
        energyDisp = GameObject.Find("EnergyDisp").GetComponent<Text>();
        mineralDisp = GameObject.Find("MineralDisp").GetComponent<Text>();
        gasDisp = GameObject.Find("GasDisp").GetComponent<Text>();
        populationDisp = GameObject.Find("PopDisp").GetComponent<Text>();
        UnitPanel.gameObject.SetActive(false);
        BuildingPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        primary = GameObject.Find("Player").GetComponent<InputManager>().selectedObject.GetComponent<WorldObject>();
        if (primary.CompareTag("Building"))
        {

            UnitPanel.gameObject.SetActive(false);
            BuildingPanel.gameObject.SetActive(true);
        }
        else if (primary.CompareTag("Unit"))
        {
            BuildingPanel.gameObject.SetActive(false);
            UnitPanel.gameObject.SetActive(true);
        }

        /*if (primary.maxEnergy <= 0)
        {
             energyBar.gameObject.SetActive(false);
        }*/

        healthBar.maxValue = primary.maxHealth;
        healthBar.value = primary.health;

        /*energyBar.maxValue = primary.maxEnergy;
        energyBar.value = primary.energy;
        */
        nameDisp.text = primary.objectName;
        healthDisp.text = "HP: " + primary.health;

        attackDisp.text = "ATK: " + primary.attack;
        defenseDisp.text = "DEF: " + primary.defense;

        
       
            //energyDisp.text = "EP: " + primary.energy;


    }
}
