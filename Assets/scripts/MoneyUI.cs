using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public EconomyManager economyManager;
    public TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        economyManager.OnMoneyChanged += UpdateUI;
    }

    private void OnDisable()
    {
        economyManager.OnMoneyChanged -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI(economyManager.CurrentMoney);
    }

    private void UpdateUI(int amount)
    {
        moneyText.text = "$ " + amount.ToString();
    }
}