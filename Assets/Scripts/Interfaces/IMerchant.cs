using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMerchant 
{
    public bool PurchaseItem(int itemID);
    public void SellItem(int itemID);


}
