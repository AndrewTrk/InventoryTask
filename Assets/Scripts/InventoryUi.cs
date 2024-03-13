using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{

    [SerializeField] private Image sk1Image;
    [SerializeField] private Image sk2Image;
    private bool shopkeeperbuttonState;

    private PlayerInventory inventory;
    private ShopkeeperOneInventory shopkeeperOneInventory;
    private ShopkeeperTwoInventory shopkeeperTwoInventory;
    //player data
    [SerializeField] private Transform BackbackPanel;
    [SerializeField] private Transform templateItem;
    [SerializeField] private List<Transform> InstantiatedItems;
    
    // shopkeeperone data
    [SerializeField] private Transform skOneBackbackPanel;
    [SerializeField] private Transform skOnetemplateItem;
    [SerializeField] private List<Transform> skOneInstantiatedItems;
    
    //shopkeepertwo data
    [SerializeField] private Transform skTwoBackbackPanel;
    [SerializeField] private Transform skTwotemplateItem;
    [SerializeField] private List<Transform> skTwoInstantiatedItems;


    #region Player Inventory UI
    // set the  player inventory list for the inventory ui to populated later with changes 
    public void setInventory(PlayerInventory inventory) {
        this.inventory = inventory;
        inventory.OnItemListChanged += inventory_onItemListChanged;
    }

    //remove all inventory items "essential before refreshing the state"
    private void inventory_onItemListChanged(object sender, EventArgs e)
    {
        ClearInventory();
        UpdateInventoryUI();
    }

    //instatiate all collected inventory items ui from the template and update the image and id for
    //the selling script to work properly 
    public void UpdateInventoryUI() {

        foreach (InventoryItem item in inventory.getItemsList()) {
            Transform inventoryItem = Instantiate(templateItem, BackbackPanel) ;
            inventoryItem.GetChild(0).GetComponent<Image>().sprite = item.icon;
            inventoryItem.GetChild(1).GetComponent<SellItems>().setItemID(item.id);
            
            //sell item from Player  to the shopkeeper 1 
            inventoryItem.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                GameManager.Instance.player.SellItem(item.id);
                GameManager.Instance.shopkeeperOne.PurchaseItem(item.id);
            });
            InstantiatedItems.Add(inventoryItem);
        }
    }

    //remove all inventory items
    public void ClearInventory() {
        foreach (Transform item in InstantiatedItems) {
            Destroy(item.gameObject);
        }
        InstantiatedItems.Clear();
    }
    #endregion

    #region Shopkeeper 1 Inventory UI
    // set the  player inventory list for the inventory ui to populated later with changes 
    public void setSkOneInventory(ShopkeeperOneInventory inventory)
    {
        this.shopkeeperOneInventory = inventory;
        shopkeeperOneInventory.OnItemListChanged += SkOneinventory_onItemListChanged;
    }

    //remove all inventory items "essential before refreshing the state"
    private void SkOneinventory_onItemListChanged(object sender, EventArgs e)
    {
        ClearSkOneInventory();
        UpdateSkOneInventoryUI();
    }

    //instatiate all collected inventory items ui from the template and update the image and id for
    //the selling script to work properly 
    public void UpdateSkOneInventoryUI()
    {

        foreach (InventoryItem item in shopkeeperOneInventory.getItemsList())
        {
            Transform inventoryItem = Instantiate(skOnetemplateItem, skOneBackbackPanel);
            inventoryItem.GetChild(0).GetComponent<Image>().sprite = item.icon;
            inventoryItem.GetChild(1).GetComponent<SellItems>().setItemID(item.id);
            
            //inventoryItem.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { sellFromPlayerToSkOne(item.id); });
            
            //sell item from shopkeeper 1 to the player 
            inventoryItem.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                GameManager.Instance.player.PurchaseItem(item.id);
                GameManager.Instance.shopkeeperOne.SellItem(item.id); 
            });
            skOneInstantiatedItems.Add(inventoryItem);
        }
    }

    //remove all inventory items
    public void ClearSkOneInventory()
    {
        foreach (Transform item in skOneInstantiatedItems)
        {
            Destroy(item.gameObject);
        }
        skOneInstantiatedItems.Clear();
    }
    #endregion

    #region Shopkeeper 2 Inventory UI
    // set the  Shopkeeper 2 inventory list for the inventory ui to populated later with changes 
    public void setSkTwoInventory(ShopkeeperTwoInventory inventory)
    {
        this.shopkeeperTwoInventory = inventory;
        shopkeeperTwoInventory.OnItemListChanged += SkTwoinventory_onItemListChanged;
    }

    //remove all inventory items "essential before refreshing the state"
    private void SkTwoinventory_onItemListChanged(object sender, EventArgs e)
    {
        ClearSkTwoInventory();
        UpdateSkTwoInventoryUI();
    }

    //instatiate all collected inventory items ui from the template and update the image and id for
    //the selling script to work properly 
    public void UpdateSkTwoInventoryUI()
    {

        foreach (InventoryItem item in shopkeeperTwoInventory.getItemsList())
        {
            Transform inventoryItem = Instantiate(skTwotemplateItem, skTwoBackbackPanel);
            inventoryItem.GetChild(0).GetComponent<Image>().sprite = item.icon;
            inventoryItem.GetChild(1).GetComponent<SellItems>().setItemID(item.id);
            
            //sell item from shopkeeper 2 to the player 
            inventoryItem.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                GameManager.Instance.player.PurchaseItem(item.id);
                GameManager.Instance.shopkeeperTwo.SellItem(item.id);
            });
            skTwoInstantiatedItems.Add(inventoryItem);
        }
    }

    //remove all inventory items
    public void ClearSkTwoInventory()
    {
        foreach (Transform item in skTwoInstantiatedItems)
        {
            Destroy(item.gameObject);
        }
        skTwoInstantiatedItems.Clear();
    }
    #endregion


    #region uiFunctions
    public void SwitchShopkeeperstoSellTo() {
        if (shopkeeperbuttonState)
        {
            //sell to sk1 , so switch images and click listenerr
            sk1Image.gameObject.SetActive(true);
            sk2Image.gameObject.SetActive(false);
            shopkeeperbuttonState = false;

            foreach (Transform item in InstantiatedItems)
            {
                int id = item.GetChild(1).GetComponent<SellItems>().getItemId();
                item.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { SellFromPlayerTosk1(id); });
                  }
        }
        else {
            //sell to sk2
            sk1Image.gameObject.SetActive(false);
            sk2Image.gameObject.SetActive(true);
            shopkeeperbuttonState = true;

            foreach (Transform item in InstantiatedItems)
            {
                int id = item.GetChild(1).GetComponent<SellItems>().getItemId();
                item.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { SellFromPlayerTosk2(id); });
            }

        }
    }

    private void SellFromPlayerTosk1(int id)
    {
        GameManager.Instance.player.SellItem(id);
        GameManager.Instance.shopkeeperOne.PurchaseItem(id);
    }

    private void SellFromPlayerTosk2(int id)
    {
        GameManager.Instance.player.SellItem(id);
        GameManager.Instance.shopkeeperTwo.PurchaseItem(id);
    }
    #endregion
    /*    private void sellFromPlayerToSkOne(int id)
        {
            GameManager.Instance.player.SellItem(id);
        }*/
}
