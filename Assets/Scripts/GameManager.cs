using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;
    public ShopkeeperOne shopkeeperOne;
    public ShopkeeperTwo shopkeeperTwo;

    public PlayerInventory playerInventory;
    public ShopkeeperOneInventory shopkeeperOneInventory;
    public ShopkeeperTwoInventory shopkeeperTwoInventory;
    public InventoryUi inventoryUi;
    public ResourcesManager resourcesManager;
    public static GameManager Instance { get; private set; }


    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

/*        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>())
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if (GameObject.Find("Shopkeeper1Inventory").GetComponent<ShopkeeperOneInventory>())
        {
            shopkeeperOneInventory = GameObject.Find("Shopkeeper1Inventory").GetComponent<ShopkeeperOneInventory>();
        }

        if (GameObject.Find("Shopkeeper2Inventory").GetComponent<ShopkeeperTwoInventory>())
        {
            shopkeeperTwoInventory = GameObject.Find("Shopkeeper2Inventory").GetComponent<ShopkeeperTwoInventory>();
        }*/
        inventoryUi.setInventory(playerInventory);
        inventoryUi.setSkOneInventory(shopkeeperOneInventory);
        inventoryUi.setSkTwoInventory(shopkeeperTwoInventory);

       
    }
}
