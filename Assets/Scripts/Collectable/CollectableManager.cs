using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

public class CollectableManager : Singleton<CollectableManager>
{
    public int coins;

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoin(int amount = 1){
        coins += amount;
    }
}
