using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperOne : MonoBehaviour, IMerchant
{

    // Start is called before the first frame update
    void Start()
    {
        PurchaseItem(0);
        PurchaseItem(1);
    }

    // purcahe item and add it to the ShopkeeperOne inventory items
    public bool PurchaseItem(int itemID)
    {
        GameManager.Instance.shopkeeperOneInventory.AddItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
        //return true on successful purchase
        return true;
    }

    // sell item and remove it from the ShopkeeperOne inventory items
    public void SellItem(int itemID)
    {
        GameManager.Instance.shopkeeperOneInventory.RemoveItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
    }
}
