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
    private Player owner;
    public Player Owner {
        get { return owner; }
        set {
            ChangeOwner(value);
        }
    }
    private int funds;
    public event Action<int> OnFundsChangeEvent;
    public event Action<Player> OnOwnerChangeEvent;
    public AIPlayer boardOfDirectors;

    public Company(Player founder) {
        owner = founder;
        boardOfDirectors = new AIPlayer();
        boardOfDirectors.FirstName = "Board of Directors";
    }

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
        return Owner;
    }
    public void ChangeOwner(Player newOwner) {
        owner.LoseOwnership(this);
        if(newOwner == null) {
            owner = boardOfDirectors;
        }
        OnOwnerChangeEvent?.Invoke(owner);
    }
}