using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public Sprite taserLight;
    public Sprite adrenalineLight;
    public Sprite rangedLight;
    public Sprite mentalLight;
    public Sprite concussionLight;
    public Sprite lethalityLight;
    public Sprite activeLight;
    public Sprite shieldLight;
    public Sprite marathonLight;
    public Sprite camouflageLight;
    public Sprite defenseLight;
    public GameObject connector1;
    public GameObject connector2;
    public GameObject connector3;
    public GameObject connector4;
    public GameObject connector5;
    public GameObject connector6;
    public GameObject connector7;
    public GameObject connector8;
    public GameObject connector9;
    public GameObject connector10;
    public static int funds = 0;
    public static ShopController instance = null;
    public Text bank;
    public Text taserCost;
    public Text adrenalineCost;
    public Text rangedCost;
    public Text mentalCost;
    public Text concussionCost;
    public Text lethalityCost;
    public Text activeCost;
    public Text shieldCost;
    public Text marathonCost;
    public Text camouflageCost;
    public Text defenseCost;
    public Button tase;
    public Button adrenaline;
    public Button ranged;
    public Button mental;
    public Button concussion;
    public Button lethality;
    public Button active;
    public Button shield;
    public Button marathon;
    public Button camouflage;
    public Button defense;
    public static bool taser;
    public static bool adrenalineRush;
    public static bool rangedTaser;
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
        funds = PointManager.points;
        SetBankText();
        connector1.gameObject.SetActive(false);
        connector2.gameObject.SetActive(false);
        connector3.gameObject.SetActive(false);
        connector4.gameObject.SetActive(false);
        connector5.gameObject.SetActive(false);
        connector6.gameObject.SetActive(false);
        connector7.gameObject.SetActive(false);
        connector8.gameObject.SetActive(false);
        connector9.gameObject.SetActive(false);
        connector10.gameObject.SetActive(false);
        adrenalineCost.gameObject.SetActive(false);
        activeCost.gameObject.SetActive(false);
        rangedCost.gameObject.SetActive(false);
        mentalCost.gameObject.SetActive(false);
        concussionCost.gameObject.SetActive(false);
        lethalityCost.gameObject.SetActive(false);
        shieldCost.gameObject.SetActive(false);
        marathonCost.gameObject.SetActive(false);
        camouflageCost.gameObject.SetActive(false);
        defenseCost.gameObject.SetActive(false);
        adrenaline.interactable = false;
        active.interactable = false;
        ranged.interactable = false;
        mental.interactable = false;
        concussion.interactable = false;
        lethality.interactable = false;
        shield.interactable = false;
        marathon.interactable = false;
        camouflage.interactable = false;
        defense.interactable = false;

        if (taser == true)
        {
            TaserBought();
        }
        if (adrenalineRush == true)
        {
            AdrenalineBought();
        }

        if (rangedTaser == true)
        {
            RangedTaserBought();
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

    public void Taser()
    {
        taser = true;
        tase.image.sprite = taserLight;
        tase.interactable = false;
        taserCost.gameObject.SetActive(false);
        adrenaline.interactable = true;
        active.interactable = true;
        adrenalineCost.gameObject.SetActive(true);
        activeCost.gameObject.SetActive(true);
        connector1.gameObject.SetActive(true);
        connector2.gameObject.SetActive(true);
    }

    public void AdrenalineRush()
    {
        if (funds >= 1)
        {
            funds -= 1;
            adrenaline.image.sprite = adrenalineLight;
            adrenalineRush = true;
            adrenaline.interactable = false;
            adrenalineCost.gameObject.SetActive(false);
            ranged.interactable = true;
            mental.interactable = true;
            rangedCost.gameObject.SetActive(true);
            mentalCost.gameObject.SetActive(true);
            connector3.gameObject.SetActive(true);
            connector5.gameObject.SetActive(true);
            SetBankText();
        }
    }

    public void RangedTaser()
    {
        if (funds >= 2)
        {
            funds -= 2;
            ranged.image.sprite = rangedLight;
            rangedTaser = true;
            ranged.interactable = false;
            rangedCost.gameObject.SetActive(false);
            SetBankText();
        }
    }

    public void MentalStrength()
    {
        if (funds >= 2)
        {
            funds -= 2;
            mental.image.sprite = mentalLight;
            mentalStrength = true;
            mental.interactable = false;
            mentalCost.gameObject.SetActive(false);
            concussion.interactable = true;
            concussionCost.gameObject.SetActive(true);
            connector7.gameObject.SetActive(true);
            SetBankText();
        }
    }

    public void ConcussionWave()
    {
        if (funds >= 3)
        {
            funds -= 3;
            concussion.image.sprite = concussionLight;
            concussionWave = true;
            concussion.interactable = false;
            concussionCost.gameObject.SetActive(false);
            lethality.interactable = true;
            lethalityCost.gameObject.SetActive(true);
            connector9.gameObject.SetActive(true);
            SetBankText();
        }
    }

    public void FocusedLethality()
    {
        if (funds >= 4)
        {
            funds -= 4;
            lethality.image.sprite = lethalityLight;
            focusedLethality = true;
            lethality.interactable = false;
            lethalityCost.gameObject.SetActive(false);
            SetBankText();
        }
    }

    public void ActiveBody()
    {
        if (funds >= 1)
        {
            funds -= 1;
            active.image.sprite = activeLight;
            activeBody = true;
            active.interactable = false;
            activeCost.gameObject.SetActive(false);
            shield.interactable = true;
            marathon.interactable = true;
            shieldCost.gameObject.SetActive(true);
            marathonCost.gameObject.SetActive(true);
            connector4.gameObject.SetActive(true);
            connector6.gameObject.SetActive(true);
            SetBankText();
        }
    }

    public void EnergyShield()
    {
        if (funds >= 2)
        {
            funds -= 2;
            shield.image.sprite = shieldLight;
            energyShield = true;
            shield.interactable = false;
            shieldCost.gameObject.SetActive(false);
            SetBankText();
        }
    }

    public void MarathonRunner()
    {
        if (funds >= 2)
        {
            funds -= 2;
            marathon.image.sprite = marathonLight;
            marathonRunner = true;
            marathon.interactable = false;
            marathonCost.gameObject.SetActive(false);
            camouflage.interactable = true;
            camouflageCost.gameObject.SetActive(true);
            connector8.gameObject.SetActive(true);
            SetBankText();
        }
    }

    public void ActiveCamouflage()
    {
        if (funds >= 3)
        {
            funds -= 3;
            camouflage.image.sprite = camouflageLight;
            activeCamouflage = true;
            camouflage.interactable = false;
            camouflageCost.gameObject.SetActive(false);
            defense.interactable = true;
            defenseCost.gameObject.SetActive(true);
            connector10.gameObject.SetActive(true);
            SetBankText();
        }
    }

    public void FocusedDefense()
    {
        if (funds >= 4)
        {
            funds -= 4;
            defense.image.sprite = defenseLight;
            focusedDefense = true;
            defense.interactable = false;
            defenseCost.gameObject.SetActive(false);
            SetBankText();
        }
    }

    void TaserBought()
    {
        tase.interactable = false;
        tase.image.sprite = taserLight;
        taserCost.gameObject.SetActive(false);
        adrenaline.interactable = true;
        active.interactable = true;
        adrenalineCost.gameObject.SetActive(true);
        activeCost.gameObject.SetActive(true);
        connector1.gameObject.SetActive(true);
        connector2.gameObject.SetActive(true);
    }

    void AdrenalineBought()
    {
        adrenaline.interactable = false;
        adrenaline.image.sprite = adrenalineLight;
        adrenalineCost.gameObject.SetActive(false);
        ranged.interactable = true;
        mental.interactable = true;
        rangedCost.gameObject.SetActive(true);
        mentalCost.gameObject.SetActive(true);
        connector3.gameObject.SetActive(true);
        connector5.gameObject.SetActive(true);
    }

    void RangedTaserBought()
    {
        ranged.interactable = false;
        ranged.image.sprite = rangedLight;
        rangedCost.gameObject.SetActive(false);
    }

    void MentalBought()
    {
        mental.interactable = false;
        mental.image.sprite = mentalLight;
        mentalCost.gameObject.SetActive(false);
        concussion.interactable = true;
        concussionCost.gameObject.SetActive(true);
        connector7.gameObject.SetActive(true);
    }

    void ConcussionBought()
    {
        concussion.interactable = false;
        concussion.image.sprite = concussionLight;
        concussionCost.gameObject.SetActive(false);
        lethality.interactable = true;
        lethalityCost.gameObject.SetActive(true);
        connector9.gameObject.SetActive(true);
    }

    void LethalityBought()
    {
        lethality.interactable = false;
        lethality.image.sprite = lethalityLight;
        lethalityCost.gameObject.SetActive(false);
    }

    void ActiveBoughty()
    {
        active.interactable = false;
        active.image.sprite = activeLight;
        activeCost.gameObject.SetActive(false);
        shield.interactable = true;
        marathon.interactable = true;
        shieldCost.gameObject.SetActive(true);
        marathonCost.gameObject.SetActive(true);
        connector4.gameObject.SetActive(true);
        connector6.gameObject.SetActive(true);
    }

    void ShieldBought()
    {
        shield.interactable = false;
        shield.image.sprite = shieldLight;
        shieldCost.gameObject.SetActive(false);
    }

    void MarathonBought()
    {
        marathon.interactable = false;
        marathon.image.sprite = marathonLight;
        marathonCost.gameObject.SetActive(false);
        camouflage.interactable = true;
        camouflageCost.gameObject.SetActive(true);
        connector8.gameObject.SetActive(true);
    }

    void CamouflageBought()
    {
        camouflage.interactable = false;
        camouflage.image.sprite = camouflageLight;
        camouflageCost.gameObject.SetActive(false);
        defense.interactable = true;
        defenseCost.gameObject.SetActive(true);
        connector10.gameObject.SetActive(true);
    }

    void DefenseBought()
    {
        defense.interactable = false;
        defense.image.sprite = defenseLight;
        defenseCost.gameObject.SetActive(false);
    }

    public void ReturnToGame()
    {
        PointManager.points = funds;
        SceneManager.LoadScene(sceneName: "Inventory");
    }

    void SetBankText()
    {
        bank.text = "Skill Points: " + funds.ToString();
    }
}