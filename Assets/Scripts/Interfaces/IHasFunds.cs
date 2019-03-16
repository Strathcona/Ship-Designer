using UnityEngine;
using System.Collections;
using System;

public interface IHasFunds {
    event Action<int> OnFundsChangeEvent;
    int Funds { get; set; }
}


