using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItems : MonoBehaviour
{
    private int itemID;

    void Start()
    {
        
    }
    public int getItemId() {
        return itemID;
    }
    public void setItemID(int id) {
        itemID = id;
    }

}
