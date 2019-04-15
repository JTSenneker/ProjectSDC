using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject inventoryMenu;
    public bool inventoryOpen = false;
    private Rigidbody rb;
    public Text playerMoney;
    public Text playerTreasure;
    public Text inventoryMiscText;
    public Text slot1;
    public Text slot2;
    public Text slot3;
    public Text slot4;
    public Text slot5;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            if (MoneyManager.inventory[i] == null)
            {
                MoneyManager.inventory[i] = "Empty";
            }
        }
        rb = GetComponent<Rigidbody>();
        inventoryMenu.gameObject.SetActive(false);
        inventoryMiscText.gameObject.SetActive(false);
        SetAllText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryOpen == false)
            {
                inventoryMenu.gameObject.SetActive(true);
                inventoryOpen = true;
            }

            else if (inventoryOpen == true)
            {
                inventoryMenu.gameObject.SetActive(false);
                inventoryOpen = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            other.gameObject.SetActive(false);
            MoneyManager.money += other.gameObject.GetComponent<ObjectValue>().value;
            SetMoneyText();
        }

        else if (other.gameObject.CompareTag("Treasure"))
        {
            for (int i = 0; i < 5; i++)
            {
                if (MoneyManager.inventory[i] == "Empty")
                {
                    other.gameObject.SetActive(false);
                    MoneyManager.treasure++;
                    MoneyManager.inventory[i] = other.name;
                    MoneyManager.worth[i] = other.GetComponent<ObjectValue>().value;
                    break;
                }

                if (MoneyManager.inventory[0] != "Empty" && MoneyManager.inventory[1] != "Empty" && MoneyManager.inventory[2] != "Empty" && MoneyManager.inventory[3] != "Empty" && MoneyManager.inventory[4] != "Empty")
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
            if (MoneyManager.inventory[0] != "Empty")
            {
                FindObjectOfType<MoneyManager>().Sell();
                inventoryMiscText.gameObject.SetActive(true);
            }
        }

        if (other.gameObject.CompareTag("Shop"))
        {
            SceneManager.LoadScene(sceneName: "Shop");
        }
    }

    void OnTriggerExit()
    {
        inventoryMiscText.gameObject.SetActive(false);
    }

    public void SetMoneyText()
    {
        playerMoney.text = "Money: " + MoneyManager.money.ToString();
    }

    public void SetTreasureText()
    {
        playerTreasure.text = "Treasure: " + MoneyManager.treasure.ToString();
    }

    public void SetInventorytext()
    {
        slot1.text = MoneyManager.inventory[0];
        slot2.text = MoneyManager.inventory[1];
        slot3.text = MoneyManager.inventory[2];
        slot4.text = MoneyManager.inventory[3];
        slot5.text = MoneyManager.inventory[4];
    }

    public void SetAllText()
    {
        SetMoneyText();
        SetTreasureText();
        SetInventorytext();
    }
}
