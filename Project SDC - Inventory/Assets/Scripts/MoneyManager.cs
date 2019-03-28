using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static string[] inventory = new string[5];
    public static int[] worth = new int[5];
    public static int money = 0;
    public int sold = 0;
    public static int treasure = 0;

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
    }

    public void Sell()
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
                FindObjectOfType<PlayerController>().inventoryMiscText.text = "Sold 1 treasure for " + sold + " gold";
            }

            else
            {
                FindObjectOfType<PlayerController>().inventoryMiscText.text = "Sold " + treasure + " treasures for " + sold + " gold";
            }

            money += sold;
            treasure = 0;
            FindObjectOfType<PlayerController>().SetAllText();
        }
    }
}
