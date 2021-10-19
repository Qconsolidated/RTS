using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    public GameObject selectionIndicator;
    public CanvasGroup InfoPanel;
    public bool isSelected = false;
    public bool isUnit;
 
    public int health;
    public int maxHealth;
    public Slider healthBar;
    public Text healthDisp;

    public int energy;
    public int maxEnergy;
    public Slider energyBar;
    public Text energyDisp;

    public int attack;
    public int defense;
    public int kills;
    public Text attackDisp;
    public Text defenseDisp;
    public Text killDisp;


    public string objectName;
    public Text nameDisp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       selectionIndicator.SetActive(isSelected);
        
        if (maxEnergy <= 0)
        {
            energyBar.gameObject.SetActive(false);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        healthBar.maxValue = maxHealth;
        healthBar.value = health;

        energyBar.maxValue = maxEnergy;
        energyBar.value = energy;

      

        nameDisp.text = objectName;
        healthDisp.text = "HP: " + health;

        energyDisp.text = "EP: " + health;
    }
}
