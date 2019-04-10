using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Magnate : Person, IHasFunds {

    private Company activeCompany;
    public Company ActiveCompany {
        get { return activeCompany; }
        set {
            activeCompany = value;
            OnActiveCompanyChangeEvent?.Invoke(activeCompany);
        }
    }

    private int funds;
    public int Funds {
        get { return funds; }
        set {
            funds = value;
            OnFundsChangeEvent?.Invoke(Funds);
        }
    }

    public void GainOwnership(IHasOwner thing) {
        thing.ChangeOwner(this);
        ownedEntities.Add(thing);
    }
    public void LoseOwnership(IHasOwner thing) {
        ownedEntities.Remove(thing);
    }
    public bool TryToPurchase(IHasCost purchase) {
        if (Funds > purchase.Cost) {
            Funds -= purchase.Cost;
            return true;
        } else {
            return false;
        }
    }


    public event Action<int> OnFundsChangeEvent;
    public event Action<Company> OnActiveCompanyChangeEvent;
    public List<IHasOwner> ownedEntities = new List<IHasOwner>();
}
