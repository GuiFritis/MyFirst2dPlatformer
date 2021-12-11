using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollectablePlanet : ItemCollectableBase
{

    public SOAnimation collectingMoveY;
    public SOAnimation collectingScaleX;
    public SOAnimation collectingScale;
    public SOAnimation collectingFade;

    public SpriteRenderer planetSprite;

    protected override void Collect()
    {
        
        collectingMoveY.DGAnimate(transform.DOMoveY(collectingMoveY.value , collectingMoveY.duration));
        collectingScaleX.DGAnimate(transform.DOScaleX(transform.localScale.x * collectingScaleX.value, collectingScaleX.duration));
        collectingScale.DGAnimate(transform.DOScale(transform.localScale * collectingScale.value, collectingScale.duration));
        collectingFade.DGAnimate(planetSprite.DOFade(collectingFade.value, collectingFade.duration));
        Invoke(nameof(Disactivate), collectingMoveY.duration+collectingFade.duration);
        OnCollect();
    }

    private void Disactivate(){
        gameObject.SetActive(false);
    }
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddEnergy(15);
    }
}
