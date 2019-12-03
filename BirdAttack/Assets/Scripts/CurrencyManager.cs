using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : AwakeSingleton<CurrencyManager>
{
    [SerializeField]
    private float _bank = 200;
    public float Bank
    {
        get { return _bank; }
    }

    [HideInInspector]
    public UnityEvent OnMoney = new UnityEvent();

    public void AddToBank(float amount)
    {
        _bank += amount;
        OnMoney.Invoke();
    }

    public void Pay(float amount)
    {
        _bank -= amount;
        OnMoney.Invoke();
    }
}