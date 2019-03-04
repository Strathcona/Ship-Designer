using System.Collections;
using System.Collections.Generic;
using System;
using Funds;

public class Player: IHasFunds {
    public string firstName;
    public string lastName;
    public Portrait portait;
    public List<IHasOwner> ownedEntities;
    public event Action<int> OnFundsChangeEvent;

    public void GainOwnership(IHasOwner thing) {
        thing.ChangeOwner(this);
        ownedEntities.Add(thing);
    }
    public void LoseOwnership(IHasOwner thing) {
        ownedEntities.Remove(thing);
    }

    private int funds;
    public int GetFunds() {
        return funds;
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
    }

}
