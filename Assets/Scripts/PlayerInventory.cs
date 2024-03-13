using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;

    private List<ScriptableObject> inventoryItems;

    public PlayerInventory() {
        inventoryItems = new List<ScriptableObject>();
    }
    public void AddItem(ScriptableObject item) {
        inventoryItems.Add(item);
        printItems();
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public void RemoveItem(ScriptableObject item)
    {
        inventoryItems.Remove(item);
        printItems();
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<ScriptableObject>  getItemsList() {
        return inventoryItems;
    }

    public void printItems() {
        Debug.Log("Items == "+ inventoryItems.Count);
    }
}
