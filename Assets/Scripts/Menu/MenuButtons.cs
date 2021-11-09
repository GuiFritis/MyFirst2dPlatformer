using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtons : MonoBehaviour
{

    [Header("Animation")]
    [SerializeField]
    private float duration = .2f;
    [SerializeField]
    private float delay = .05f;
    [SerializeField]
    private Ease ease = Ease.Linear;

    public List<GameObject> buttons;

    void Awake()
    {   
        HideAllButtons();
        ShowButtons();
    }

    private void HideAllButtons(){
        foreach (var btn in buttons)
        {
            btn.transform.localScale = Vector3.zero;
            btn.SetActive(false);
        }
    }

    private void ShowButtons(){
        foreach(var btn in buttons){
            btn.SetActive(true);
            btn.transform.DOScale(1, duration).SetEase(ease).SetDelay(delay*buttons.IndexOf(btn));
        }
    }

}
