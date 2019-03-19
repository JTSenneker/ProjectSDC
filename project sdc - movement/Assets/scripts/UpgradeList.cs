using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeList : MonoBehaviour
{
    public Dictionary<string, bool> Upgrades = new Dictionary<string, bool>();
    public List<Upgrade> upgrades = new List<Upgrade>();
    
    private int upgradeCount;
    private int currentUpgrade;
    string[] yourUpgrades = new string[5];
    Text text;
    void Start()
    {
        upgrades.Add(new ShieldUpgrade());
        text = GameObject.Find("Upgrades").GetComponent<Text>();
        Upgrades.Add("Shield", PlayerManager.hasShield);
        Upgrades.Add("Hologram", PlayerManager.hasHologram);
        Upgrades.Add("Invisibility Cloak", PlayerManager.hasInvisibilityCloak);
        Upgrades.Add("Explosive Taser", PlayerManager.hasExplosiveTaser);
        Upgrades.Add("Taser", PlayerManager.hasTaser);
        upgradeCount = 0;
        currentUpgrade = 0;
        foreach (KeyValuePair<string, bool> entry in Upgrades)
        {
            if (entry.Value == true)
            {
                yourUpgrades[upgradeCount] = entry.Key;
                upgradeCount++;
            }
        }
        text.text = yourUpgrades[0];
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentUpgrade++;
            if(currentUpgrade>=upgradeCount)
            {
                currentUpgrade = 0;
            }
            text.text = yourUpgrades[currentUpgrade];
            if(Input.GetKeyDown(KeyCode.F))
            {

            }
        }
    }
}
