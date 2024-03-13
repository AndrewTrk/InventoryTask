using System;
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
    public UIManager inventoryUi;
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

        inventoryUi.setInventory(playerInventory);
        inventoryUi.setSkOneInventory(shopkeeperOneInventory);
        inventoryUi.setSkTwoInventory(shopkeeperTwoInventory);

       
    }
}
