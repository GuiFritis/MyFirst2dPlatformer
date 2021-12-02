using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using TMPro;

public class CollectableManager : Singleton<CollectableManager>
{
    public int coins;

    public TextMeshProUGUI textCoins;

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        textCoins.text = "X" + coins;
    }

    public void AddCoin(int amount = 1){
        coins += amount;
        textCoins.text = "X" + coins;
    }
}
