using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action BalanceChanged;


    // purcahe item and add it to the player inventory items
    public bool PurchaseItem(int itemID)
    {
        
        InventoryItem itemPrice = GameManager.Instance.resourcesManager.getAvailableResources()[itemID] as InventoryItem;
        
        if (GameManager.Instance.resourcesManager.GetCoins() >= itemPrice.price)
        {

            GameManager.Instance.resourcesManager.deduceFromCoins(itemPrice.price);

            GameManager.Instance.playerInventory.AddItem(
                GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
            BalanceChanged?.Invoke();
            return true;
        }
        else {
            Debug.Log("!Sorry Not enough Cash please withdraw coins from bank account to resume your purchase");
            return false;
        }
    }

    // sell item and remove it from the player inventory items
    public void SellItem(int itemID)
    {

        InventoryItem itemPrice = GameManager.Instance.resourcesManager.getAvailableResources()[itemID] as InventoryItem;

        GameManager.Instance.playerInventory.RemoveItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);

        GameManager.Instance.resourcesManager.AddtoCoins(itemPrice.price);
        BalanceChanged?.Invoke();
    }
}
