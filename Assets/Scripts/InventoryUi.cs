using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    private PlayerInventory inventory;
    [SerializeField] private Transform BackbackPanel;
    [SerializeField] private Transform templateItem;
    [SerializeField] private List<Transform> InstantiatedItems;
    public void setInventory(PlayerInventory inventory) {
        this.inventory = inventory;
        inventory.OnItemListChanged += inventory_onItemListChanged;
    }

    private void inventory_onItemListChanged(object sender, EventArgs e)
    {
        ClearInventory();
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI() {

        foreach (InventoryItem item in inventory.getItemsList()) {
            Transform inventoryItem = Instantiate(templateItem, BackbackPanel) ;
            inventoryItem.GetChild(0).GetComponent<Image>().sprite = item.icon;
            inventoryItem.GetChild(1).GetComponent<SellItems>().setItemID(item.id);
            InstantiatedItems.Add(inventoryItem);
        }
    }

    public void ClearInventory() {
        foreach (Transform item in InstantiatedItems) {
            Destroy(item.gameObject);
        }
        InstantiatedItems.Clear();
    }
}
