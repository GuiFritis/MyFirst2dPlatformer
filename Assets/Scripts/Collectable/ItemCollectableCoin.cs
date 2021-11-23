using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollectableCoin : ItemCollectableBase
{

    public float collectingMoveY = 1f;
    public float collectingDur = 0.5f;
    public Ease colletingEase = Ease.InFlash;

    public float collectingFadeDelay = 0.2f;

    public SpriteRenderer coinSprite;

    protected override void Collect()
    {
        transform.DOMoveY(collectingMoveY, collectingDur).SetEase(colletingEase);
        coinSprite.DOFade(0, collectingDur).SetEase(colletingEase).SetDelay(collectingFadeDelay);
        Invoke(nameof(Disactivate), collectingDur+collectingFadeDelay);
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
