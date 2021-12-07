using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using TMPro;

public class CollectableManager : Singleton<CollectableManager>
{
    public int coins;

    public TextMeshProUGUI UITextCoins;

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        UITextCoins.text = "X" + coins;
    }

    public void AddCoin(int amount = 1){
        coins += amount;
        UITextCoins.text = "X" + coins;
    }
}
