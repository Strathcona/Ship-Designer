using UnityEngine;
using System.Collections;
using System;

namespace Funds {
    public interface IHasFunds {
        event Action<int> OnFundsChangeEvent;
        void ChangeFunds(int amount);
        int GetFunds();
    }
}


