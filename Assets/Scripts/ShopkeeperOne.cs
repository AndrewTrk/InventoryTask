using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperOne : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PurchaseItem(0);
        PurchaseItem(1);
    }

    // purcahe item and add it to the player inventory items
    public void PurchaseItem(int itemID)
    {
        GameManager.Instance.shopkeeperOneInventory.AddItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
    }

    // sell item and remove it from the player inventory items
    public void SellItem(int itemID)
    {
        GameManager.Instance.shopkeeperOneInventory.RemoveItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
    }
}
