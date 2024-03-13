using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [Header("Bank And Coins")]
    [SerializeField] private int coins = 1000;
    [SerializeField] private int bankCoins = 3000;

    [SerializeField] List<ScriptableObject> items;
    // axe id = 0 , car = 1 , chair  = 2 ,tree = 3
    public List<ScriptableObject> getAvailableResources() {
        return items;
    }

    public void AddtoCoins(int amount) {
        coins += amount;
        Debug.Log("Added " + amount + " Current Coins = " + coins);

    }

    public void Addtobank(int amount)
    {
        bankCoins += amount;
        Debug.Log("Added " + amount + " Current Bank balance = " + coins);

    }

    public void Addtobank10Percent()
    {
        bankCoins += bankCoins * 10 /100;
        Debug.Log("Added 10% to Current Bank balance = " + bankCoins);

    }

    public void deduceFromCoins(int amount)
    {
        coins -= amount;
        Debug.Log("deduced " + amount + " Current Coins = "+ coins);
    }

    public void deduceFromBank(int amount)
    {
        bankCoins -= amount;
        Debug.Log("deduced " + amount + " Current Bank balance = " + coins);

    }

    public int GetCoins() {
        return coins;
    }

    public int GetBankAccountBalance() {
        return bankCoins;
    }


}
