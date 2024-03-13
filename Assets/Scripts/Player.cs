using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryUi inventoryUi;
    [SerializeField] private ResourcesManager resourcesManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryUi.setInventory(playerInventory);
    }

    // Update is called once per frame
    public void PurchaseItem(int itemID)
    {
        playerInventory.AddItem(resourcesManager.getAvailableResources()[itemID]);
    }

    public void SellItem(int itemID)
    {
        playerInventory.RemoveItem(resourcesManager.getAvailableResources()[itemID]);
    }
}
