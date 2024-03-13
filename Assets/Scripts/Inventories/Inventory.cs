using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;

    private List<ScriptableObject> inventoryItems;

    public Inventory()
    {
        inventoryItems = new List<ScriptableObject>();
    }
    // add inventory items 
    public void AddItem(ScriptableObject item)
    {
        Debug.Log("item added" + item.name);
        inventoryItems.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    // remove inventory items
    public void RemoveItem(ScriptableObject item)
    {
        inventoryItems.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    // return list of items
    public List<ScriptableObject> getItemsList()
    {
        return inventoryItems;
    }
}
