using UnityEngine;
using System.Collections;
using System;

public interface IHasFunds {
    event Action<int> OnFundsChangeEvent;
    bool TryToPurchase(IHasCost purchase);
    void ChangeFunds(int amount);
    int GetFunds();
    

}
