using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company : IHasFunds {
    public string name;
    public string companyType;
    public Logo logo;
    public Player ceo;
    private int funds;
    public event Action<int> OnFundsChangeEvent;
    public bool TryToPurchase(IHasCost purchase) {
        if (funds > purchase.GetCost()) {
            ChangeFunds(purchase.GetCost());
            return true;
        } else {
            return false;
        }
    }
    public void ChangeFunds(int amount) {
        funds += amount;
    }
    public int GetFunds() {
        return funds;
    }

    public Company() {
        name = Constants.GetRandomCompanyName();
    }
}