using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    //coins UI
    [Header("coins UI")]
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text bankBalanceText;

    //Sleep ui
    [Header("Sleep UI")]
    [SerializeField] private TMP_Text sleepText;
    [SerializeField] private RectTransform sleepPanel;


    // backpack panel ui
    [Header("backpack UI")]
    [SerializeField] private Image sk1Image;
    [SerializeField] private Image sk2Image;
    private bool shopkeeperbuttonState;

    private PlayerInventory inventory;
    private ShopkeeperOneInventory shopkeeperOneInventory;
    private ShopkeeperTwoInventory shopkeeperTwoInventory;

    //player data
    [Header("player UI")]
    [SerializeField] private Transform BackbackPanel;
    [SerializeField] private Transform templateItem;
    [SerializeField] private List<Transform> InstantiatedItems;

    // shopkeeperone data
    [Header("shopkeeper 1 UI")]
    [SerializeField] private Transform skOneBackbackPanel;
    [SerializeField] private Transform skOnetemplateItem;
    [SerializeField] private List<Transform> skOneInstantiatedItems;

    //shopkeepertwo data
    [Header("shopkeeper 2 UI")]
    [SerializeField] private Transform skTwoBackbackPanel;
    [SerializeField] private Transform skTwotemplateItem;
    [SerializeField] private List<Transform> skTwoInstantiatedItems;

    private void Start()
    {
        GameManager.Instance.player.BalanceChanged += onBalanceChanged;
        onBalanceChanged();
    }

    private void onBalanceChanged()
    {
        //update coins ui
        coinsText.text = GameManager.Instance.resourcesManager.GetCoins().ToString();
        bankBalanceText.text = GameManager.Instance.resourcesManager.GetBankAccountBalance().ToString();
    }

    #region BankUI
    public void WithdrawtenDollorsFromBank()
    {
        if (GameManager.Instance.resourcesManager.GetBankAccountBalance() < 10)
        {
            Debug.Log("Sorry not enough Balance , can't withdraw cash");
            return;
        }
        GameManager.Instance.resourcesManager.deduceFromBank(10);
        GameManager.Instance.resourcesManager.AddtoCoins(10);
        onBalanceChanged();

    }

    public void DeposittenDollorsToBank()
    {
        if (GameManager.Instance.resourcesManager.GetCoins() < 10)
        {
            Debug.Log("Sorry not enough Coins , can't Deposit cash");
            return;
        }
        GameManager.Instance.resourcesManager.deduceFromCoins(10);
        GameManager.Instance.resourcesManager.Addtobank(10);
        onBalanceChanged();

    }
    #endregion

    #region Sleep
    public void StartSleepAnimation() {
        StartCoroutine(Sleep());
    }

    IEnumerator Sleep() {
        sleepText.text = "Sleeping";
        yield return new WaitForSeconds(0.5f);
        sleepText.text = "Sleeping.";
        yield return new WaitForSeconds(0.5f);
        sleepText.text = "Sleeping..";
        yield return new WaitForSeconds(0.5f);
        sleepText.text = "Sleeping...";
        yield return new WaitForSeconds(0.5f);
        sleepText.text = "Sleeping";
        yield return new WaitForSeconds(0.5f);
        sleepText.text = "Sleeping.";
        yield return new WaitForSeconds(0.5f);
        sleepText.text = "Sleeping..";
        yield return new WaitForSeconds(0.5f);
        sleepText.text = "Sleeping...";
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.resourcesManager.Addtobank10Percent();
        onBalanceChanged();
        sleepPanel.gameObject.SetActive(false);
    }
    #endregion

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
            inventoryItem.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = item.price.ToString();
            inventoryItem.GetChild(1).GetComponent<Items>().setItemID(item.id);
            
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
            inventoryItem.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = item.price.ToString();
            inventoryItem.GetChild(1).GetComponent<Items>().setItemID(item.id);
            
            //inventoryItem.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { sellFromPlayerToSkOne(item.id); });
            
            //sell item from shopkeeper 1 to the player 
            inventoryItem.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                //only remove the items from the shop if the player was able to purchase
                if (GameManager.Instance.player.PurchaseItem(item.id))
                {
                    GameManager.Instance.shopkeeperOne.SellItem(item.id);
                }
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
            inventoryItem.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = item.price.ToString();
            inventoryItem.GetChild(1).GetComponent<Items>().setItemID(item.id);
            
            //sell item from shopkeeper 2 to the player 
            inventoryItem.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                //only remove the items from the shop if the player was able to purchase
                if (GameManager.Instance.player.PurchaseItem(item.id))
                {
                    GameManager.Instance.shopkeeperTwo.SellItem(item.id);
                }
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
            //sell to sk1 , so switch images and clicklistener callback
            sk1Image.gameObject.SetActive(true);
            sk2Image.gameObject.SetActive(false);
            shopkeeperbuttonState = false;

            //switch clicklistener callback
            foreach (Transform item in InstantiatedItems)
            {
                int id = item.GetChild(1).GetComponent<Items>().getItemId();
                item.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { SellFromPlayerTosk1(id); });
            }
        }
        else {
            //sell to sk2
            sk1Image.gameObject.SetActive(false);
            sk2Image.gameObject.SetActive(true);
            shopkeeperbuttonState = true;

            //switch clicklistener callback
            foreach (Transform item in InstantiatedItems)
            {
                int id = item.GetChild(1).GetComponent<Items>().getItemId();
                item.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { SellFromPlayerTosk2(id); });
            }

        }
    }

    public void UpdateShopkeepersStatetoSellTo()
    {
        if (!shopkeeperbuttonState)
        {
            //sell to sk1 , so switch images and clicklistener callback
            sk1Image.gameObject.SetActive(true);
            sk2Image.gameObject.SetActive(false);
            //switch clicklistener callback
            foreach (Transform item in InstantiatedItems)
            {
                int id = item.GetChild(1).GetComponent<Items>().getItemId();
                item.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { SellFromPlayerTosk1(id); });
            }
        }
        else
        {
            //sell to sk2
            sk1Image.gameObject.SetActive(false);
            sk2Image.gameObject.SetActive(true);
            //switch clicklistener callback
            foreach (Transform item in InstantiatedItems)
            {
                int id = item.GetChild(1).GetComponent<Items>().getItemId();
                item.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { SellFromPlayerTosk2(id); });
            }

        }
    }

    private void SellFromPlayerTosk1(int id)
    {
        GameManager.Instance.player.SellItem(id);
        GameManager.Instance.shopkeeperOne.PurchaseItem(id);
        UpdateShopkeepersStatetoSellTo();
    }

    private void SellFromPlayerTosk2(int id)
    {
        GameManager.Instance.player.SellItem(id);
        GameManager.Instance.shopkeeperTwo.PurchaseItem(id);
        UpdateShopkeepersStatetoSellTo();
    }
    #endregion
    /*    private void sellFromPlayerToSkOne(int id)
        {
            GameManager.Instance.player.SellItem(id);
        }*/
}
