using UnityEngine;
using System.Collections;
using System;
public static class Funds {
    public static bool TransferFunds(IHasFunds from, IHasFunds to, int amount, bool force = false) {
        if (force) {
            from.Funds -= amount;
            to.Funds += amount;
            return true;
        }
        if (from.Funds < amount) {
            return false;
        } else {
            from.Funds -= amount;
            to.Funds += amount;
            return true;
        }
    }

    public static bool TryToPurchase(IHasFunds purchaser, IHasCost purchase, bool force = false) {
        if (force) {
            purchaser.Funds -= purchase.Cost;
            return true;
        }
        if (purchaser.Funds < purchase.Cost) {
            return false;
        } else {
            purchaser.Funds -= purchase.Cost;
            return true;
        }
    }
}

