using UnityEngine;
using System;

public class EconomyManager : MonoBehaviour
{
    public int startMoney = 100;
    public int CurrentMoney { get; private set; }

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        CurrentMoney = startMoney;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public bool CanAfford(int amount)
    {
        return CurrentMoney >= amount;
    }

    public void Spend(int amount)
    {
        CurrentMoney -= amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public void Add(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }
}