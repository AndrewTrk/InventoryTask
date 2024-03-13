using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperTwo : MonoBehaviour, IMerchant
{
    // Start is called before the first frame update
    void Start()
    {
        PurchaseItem(2);
        PurchaseItem(3);
    }

    // purcahe item and add it to the ShopkeeperTwo inventory items
    public bool PurchaseItem(int itemID)
    {
        GameManager.Instance.shopkeeperTwoInventory.AddItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
        //return true on successful purchase
        return true;
    }

    // sell item and remove it from the ShopkeeperTwo inventory items
    public void SellItem(int itemID)
    {
        GameManager.Instance.shopkeeperTwoInventory.RemoveItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
    }
}
