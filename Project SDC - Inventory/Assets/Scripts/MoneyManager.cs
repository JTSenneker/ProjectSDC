using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static string[] inventory = new string[5];
    public static int[] worth = new int[5];
    public static int money = 0;
    public int sold = 0;
    public static int treasure = 0;
    public Text playerMoney;
    public Text playerTreasure;
    public Text inventoryMiscText;
    public Text slot1;
    public Text slot2;
    public Text slot3;
    public Text slot4;
    public Text slot5;

    public static MoneyManager instance = null;

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
        money = ShopController.funds;
        for (int i = 0; i < 5; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = "Empty";
            }
        }
        inventoryMiscText.gameObject.SetActive(false);
        SetAllText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            other.gameObject.SetActive(false);
            money += other.gameObject.GetComponent<ObjectValue>().value;
            SetMoneyText();
        }

        else if (other.gameObject.CompareTag("Treasure"))
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == "Empty")
                {
                    other.gameObject.SetActive(false);
                    treasure++;
                    inventory[i] = other.name;
                    worth[i] = other.GetComponent<ObjectValue>().value;
                    break;
                }

                if (inventory[0] != "Empty" && inventory[1] != "Empty" && inventory[2] != "Empty" && inventory[3] != "Empty" && inventory[4] != "Empty")
                {
                    inventoryMiscText.text = "Inventory Full";
                    inventoryMiscText.gameObject.SetActive(true);
                }
            }
            SetTreasureText();
            SetInventorytext();
        }

        else if (other.gameObject.CompareTag("Pawn"))
        {
            if (treasure != 0)
            {
                sold = 0;
                for (int i = 0; i < 5; i++)
                {
                    sold += worth[i];
                    inventory[i] = "Empty";
                    worth[i] = 0;
                }

                if (treasure == 1)
                {
                    inventoryMiscText.text = "Sold 1 treasure for " + sold + " gold";
                }

                else
                {
                    inventoryMiscText.text = "Sold " + treasure + " treasures for " + sold + " gold";
                }

                inventoryMiscText.gameObject.SetActive(true);
                money += sold;
                treasure = 0;
                SetAllText();
            }
        }
    }

    void OnTriggerExit()
    {
        inventoryMiscText.gameObject.SetActive(false);
    }

    void SetMoneyText()
    {
        playerMoney.text = "Money: " + money.ToString();
    }

    void SetTreasureText()
    {
        playerTreasure.text = "Treasure: " + treasure.ToString();
    }

    void SetInventorytext()
    {
        slot1.text = inventory[0];
        slot2.text = inventory[1];
        slot3.text = inventory[2];
        slot4.text = inventory[3];
        slot5.text = inventory[4];
    }

    void SetAllText()
    {
        SetMoneyText();
        SetTreasureText();
        SetInventorytext();
    }
}
