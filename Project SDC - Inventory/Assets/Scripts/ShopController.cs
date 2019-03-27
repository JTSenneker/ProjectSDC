using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public static int funds = 0;
    public static ShopController instance = null;
    public Text bank;
    public Text adrenalineCost;
    public Text longRangeCost;
    public Text mentalCost;
    public Text concussionCost;
    public Text lethalityCost;
    public Text activeCost;
    public Text shieldCost;
    public Text marathonCost;
    public Text camouflageCost;
    public Text defenseCost;
    public Button adrenaline;
    public Button longRange;
    public Button mental;
    public Button concussion;
    public Button lethality;
    public Button active;
    public Button shield;
    public Button marathon;
    public Button camouflage;
    public Button defense;
    public static bool adrenalineRush;
    public static bool longRangeTaser;
    public static bool mentalStrength;
    public static bool concussionWave;
    public static bool focusedLethality;
    public static bool activeBody;
    public static bool energyShield;
    public static bool marathonRunner;
    public static bool activeCamouflage;
    public static bool focusedDefense;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        funds = MoneyManager.money;
        SetBankText();
        longRange.interactable = false;
        mental.interactable = false;
        concussion.interactable = false;
        lethality.interactable = false;
        shield.interactable = false;
        marathon.interactable = false;
        camouflage.interactable = false;
        defense.interactable = false;

        if (adrenalineRush == true)
        {
            AdrenalineBought();
        }

        if (longRangeTaser == true)
        {
            LongRangeBought();
        }

        if (mentalStrength == true)
        {
            MentalBought();
        }

        if (concussionWave == true)
        {
            ConcussionBought();
        }

        if (focusedLethality == true)
        {
            LethalityBought();
        }

        if (activeBody == true)
        {
            ActiveBoughty();
        }

        if (energyShield == true)
        {
            ShieldBought();
        }

        if (marathonRunner == true)
        {
            MarathonBought();
        }

        if (activeCamouflage == true)
        {
            CamouflageBought();
        }

        if (focusedDefense == true)
        {
            DefenseBought();
        }
    }

    public void AdrenalineRush()
    {
        if (funds >= 500)
        {
            funds -= 500;
            adrenalineRush = true;
            adrenaline.interactable = false;
            adrenalineCost.text = "SOLD";
            longRange.interactable = true;
            mental.interactable = true;
            SetBankText();
        }
    }

    public void LongRangeTaser()
    {
        if (funds >= 5000)
        {
            funds -= 5000;
            longRangeTaser = true;
            longRange.interactable = false;
            longRangeCost.text = "SOLD";
            SetBankText();
        }
    }

    public void MentalStrength()
    {
        if (funds >= 2000)
        {
            funds -= 2000;
            mentalStrength = true;
            mental.interactable = false;
            mentalCost.text = "SOLD";
            concussion.interactable = true;
            SetBankText();
        }
    }

    public void ConcussionWave()
    {
        if (funds >= 5000)
        {
            funds -= 5000;
            concussionWave = true;
            concussion.interactable = false;
            concussionCost.text = "SOLD";
            lethality.interactable = true;
            SetBankText();
        }
    }

    public void FocusedLethality()
    {
        if (funds >= 10000)
        {
            funds -= 10000;
            focusedLethality = true;
            lethality.interactable = false;
            lethalityCost.text = "SOLD";
            SetBankText();
        }
    }

    public void ActiveBody()
    {
        if (funds >= 500)
        {
            funds -= 500;
            activeBody = true;
            active.interactable = false;
            activeCost.text = "SOLD";
            shield.interactable = true;
            marathon.interactable = true;
            SetBankText();
        }
    }

    public void EnergyShield()
    {
        if (funds >= 5000)
        {
            funds -= 5000;
            energyShield = true;
            shield.interactable = false;
            shieldCost.text = "SOLD";
            SetBankText();
        }
    }

    public void MarathonRunner()
    {
        if (funds >= 2000)
        {
            funds -= 2000;
            marathonRunner = true;
            marathon.interactable = false;
            marathonCost.text = "SOLD";
            camouflage.interactable = true;
            SetBankText();
        }
    }

    public void ActiveCamouflage()
    {
        if (funds >= 5000)
        {
            funds -= 5000;
            activeCamouflage = true;
            camouflage.interactable = false;
            camouflageCost.text = "SOLD";
            defense.interactable = true;
            SetBankText();
        }
    }

    public void FocusedDefense()
    {
        if (funds >= 10000)
        {
            funds -= 10000;
            focusedDefense = true;
            defense.interactable = false;
            defenseCost.text = "SOLD";
            SetBankText();
        }
    }

    void AdrenalineBought()
    {
        adrenaline.interactable = false;
        adrenalineCost.text = "SOLD";
        longRange.interactable = true;
        mental.interactable = true;
    }

    void LongRangeBought()
    {
        longRange.interactable = false;
        longRangeCost.text = "SOLD";
    }

    void MentalBought()
    {
        mental.interactable = false;
        mentalCost.text = "SOLD";
        concussion.interactable = true;
    }

    void ConcussionBought()
    {
        concussion.interactable = false;
        concussionCost.text = "SOLD";
        lethality.interactable = true;
    }

    void LethalityBought()
    {
        lethality.interactable = false;
        lethalityCost.text = "SOLD";
    }

    void ActiveBoughty()
    {
        active.interactable = false;
        activeCost.text = "SOLD";
        shield.interactable = true;
        marathon.interactable = true;
    }

    void ShieldBought()
    {
        shield.interactable = false;
        shieldCost.text = "SOLD";
    }

    void MarathonBought()
    {
        marathon.interactable = false;
        marathonCost.text = "SOLD";
        camouflage.interactable = true;
    }

    void CamouflageBought()
    {
        camouflage.interactable = false;
        camouflageCost.text = "SOLD";
        defense.interactable = true;
    }

    void DefenseBought()
    {
        defense.interactable = false;
        defenseCost.text = "SOLD";
    }

    public void ReturnToGame()
    {
        SceneManager.LoadScene(sceneName: "Inventory");
    }

    void SetBankText()
    {
        bank.text = "Money: " + funds.ToString();
    }
}