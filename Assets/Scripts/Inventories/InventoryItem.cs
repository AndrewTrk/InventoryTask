using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Inventory item")]
public class InventoryItem : ScriptableObject
{
    public int id;
    public Sprite icon;
    public int price;


}
