using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Funds;
using GameConstructs;

public class Company : IHasFunds, IHasOwner{
    public string name;
    public string companyType;
    public Logo logo;
    public Player owner;
    private int funds;
    public event Action<int> OnFundsChangeEvent;
    public event Action<Player> OnOwnerChangeEvent;
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
        OnFundsChangeEvent?.Invoke(funds);
    }
    public int GetFunds() {
        return funds;
    }
    public Player GetOwner() {
        return owner;
    }
    public void ChangeOwner(Player newOwner) {
        owner.LoseOwnership(this);
        owner = newOwner;
        OnOwnerChangeEvent?.Invoke(owner);
    }
    public Company() {
        name = Constants.GetRandomCompanyName();
    }
}