using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBase : MonoBehaviour
{
    public int startLife = 5;

    public bool destroyOnKill = false;

    public float delayToKill = 0f;

    public Action OnDeath;

    private int _curLife;

    private bool _isDead = false;

    [SerializeField]
    private FlashColor _flashColor;

    void Awake()
    {
        Init();
        if(_flashColor == null){
            _flashColor = GetComponent<FlashColor>();
        }
    }

    void Init()
    {
        _isDead = false;
        _curLife = startLife;
    }

    public void TakeDamage(int damage){

        if(_isDead){
            return;
        }

        _curLife -= damage;

        if(_curLife <= 0){
            Kill();
        }

        if(_flashColor != null){
            _flashColor.Flash();
        }
    }

    private void Kill(){
        _isDead = true;

        if(destroyOnKill){
            Destroy(gameObject, delayToKill);
        }

        if(OnDeath != null){
            OnDeath.Invoke();
        }
    }
}
