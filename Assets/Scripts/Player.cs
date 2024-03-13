using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // purcahe item and add it to the player inventory items
    public void PurchaseItem(int itemID)
    {
        GameManager.Instance.playerInventory.AddItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
    }

    // sell item and remove it from the player inventory items
    public void SellItem(int itemID)
    {
        GameManager.Instance.playerInventory.RemoveItem(
            GameManager.Instance.resourcesManager.getAvailableResources()[itemID]);
    }
}
