using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollectableCoin : ItemCollectableBase
{

    public SOAnimation collectingMoveY;
    public SOAnimation collectingFade;

    public SpriteRenderer coinSprite;

    protected override void Collect()
    {
        collectingMoveY.DGAnimate(transform.DOMoveY(collectingMoveY.value , collectingMoveY.duration));
        collectingFade.DGAnimate(coinSprite.DOFade(collectingFade.value, collectingFade.duration));
        Invoke(nameof(Disactivate), collectingMoveY.duration+collectingFade.duration);
        OnCollect();
    }

    private void Disactivate(){
        gameObject.SetActive(false);
    }
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddCoin();
    }
}
