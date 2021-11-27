using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;

    public Color flashColor = Color.red;
    public float falshDuration = .3f;

    private Tween _curTween;

    void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach(var child in transform.GetComponentsInChildren<SpriteRenderer>()){
            spriteRenderers.Add(child);
        }
    }

    public void Flash(){

        if(_curTween != null){
            _curTween.Kill();
            spriteRenderers.ForEach(s => s.color = Color.white);
        }
        
        foreach(var s in spriteRenderers){
            _curTween = s.DOColor(flashColor, falshDuration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
