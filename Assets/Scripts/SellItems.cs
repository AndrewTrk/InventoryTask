using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItems : MonoBehaviour
{
    private int itemID;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(sellItem);
    }

    private void sellItem()
    {
        GameManager.Instance.player.SellItem(itemID);
    }

    public void setItemID(int id) {
        itemID = id;
    }

}
