using System.Collections;
using System.Collections.Generic;
using System;

public class Player: IHasFunds {
    public string firstName;
    public string lastName;
    public Portrait portait;
    public Company ownedCompany;
    public event Action<int> OnFundsChangeEvent;
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
