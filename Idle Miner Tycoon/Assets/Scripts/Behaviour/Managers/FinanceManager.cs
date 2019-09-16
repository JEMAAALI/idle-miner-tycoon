using UnityEngine;
using UnityEngine.Events;

public class FinanceManager : MonoBehaviour
{
    [SerializeField] public double totalMoney;
    [SerializeField] private GameSettings _settings;
    [SerializeField] public float _nextShaftPrice;
    
    public UnityEvent MoneyUpdated;
    
    public double TotalMoney
    {
        get { return totalMoney; }
    }

    public GameSettings Settings
    {
        get { return _settings; }
    }

    public float NextShaftPrice
    {
        get { return _nextShaftPrice; }
        set { _nextShaftPrice = value; }
    }

    public void UpdateMoney(double amount)
    {
        totalMoney += amount;
        MoneyUpdated.Invoke();
    }
}