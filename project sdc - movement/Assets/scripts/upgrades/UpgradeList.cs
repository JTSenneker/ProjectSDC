using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeList : MonoBehaviour
{
    public Dictionary<string, bool> Upgrades = new Dictionary<string, bool>();
    public List<Upgrade> upgrades = new List<Upgrade>();

    PlayerMovement playerMovement;
    private int upgradeCount;
    public int currentUpgrade;
    string[] yourUpgrades = new string[5];
    Text text;
    void Start()
    {
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
        upgrades.Add(new ShieldUpgrade());
        upgrades.Add(new HologramUpgrade());
        upgrades.Add(new InvisibilityUpgrade());
        upgrades.Add(new ExplodingTaserUpgrade());
        upgrades.Add(new TaserUpgrade());
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
        if(Input.GetKeyDown(KeyCode.Q) && !playerMovement.active)
        {
            currentUpgrade++;
            if(currentUpgrade>=upgradeCount)
            {
                currentUpgrade = 0;
            }
            text.text = yourUpgrades[currentUpgrade];
        }
    }
}
