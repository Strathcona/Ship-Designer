using System.Collections;
using System.Collections.Generic;
using System;
using Funds;

public class Player: IHasFunds {
    private string title;
    public string Title {
        get { return title; }
        set { title = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    private string firstName;
    public string FirstName {
        get { return firstName; }
        set {
            firstName = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    private string lastName;
    public string LastName {
        get { return lastName; }
        set {
            lastName = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    private Portrait portrait;
    public Portrait Portrait {
        get { return portrait; }
        set {
            portrait = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    private Company activeCompany;
    public Company ActiveCompany {
        get { return activeCompany; }
        set { activeCompany = value;
            OnActiveCompanyChangeEvent?.Invoke(activeCompany);
        }
    }
    public List<IHasOwner> ownedEntities = new List<IHasOwner>();
    public event Action<Player> OnPlayerDetailsChangeEvent;
    public event Action<int> OnFundsChangeEvent;
    public event Action<Company> OnActiveCompanyChangeEvent;

    public void GainOwnership(IHasOwner thing) {
        thing.ChangeOwner(this);
        ownedEntities.Add(thing);
    }
    public void LoseOwnership(IHasOwner thing) {
        ownedEntities.Remove(thing);
    }

    private int Funds {
        get { return Funds; }
        set { Funds = value;
            OnFundsChangeEvent?.Invoke(Funds);
        }
    }
    public int GetFunds() {
        return Funds;
    }
    public bool TryToPurchase(IHasCost purchase) {
        if (Funds > purchase.GetCost()) {
            ChangeFunds(purchase.GetCost());
            return true;
        } else {
            return false;
        }
    }
    public void ChangeFunds(int amount) {
        Funds += amount;
    }

}
