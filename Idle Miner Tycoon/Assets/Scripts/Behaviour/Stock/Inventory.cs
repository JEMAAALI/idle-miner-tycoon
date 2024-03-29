﻿using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public float money;//was protected
    [SerializeField] private bool _infinite;
    [SerializeField] public TextMeshProUGUI _floatingText;//was private

    public float Money
    {
        get { return money; }
    }

    public float DrawResource(float amount)
    {
        if (_infinite)
        {
            return amount;
        }

        float result = 0f;
        if (money <= amount)
        {
            result = money;
            money = 0;
        }
        else
        {
            result = amount;
            money -= amount;
        }

        UpdateFloatingText();
        return result;
    }

    public float DrawAll()
    {
        float result = money;
        money = 0;
        UpdateFloatingText();
        return result;
    }

    public virtual void AddResource(float amount)
    {
        money += amount;
        UpdateFloatingText();
    }

    private void UpdateFloatingText()
    {
        _floatingText.text = Mathf.Round(money).ToString();
    }
}