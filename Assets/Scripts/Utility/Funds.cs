using UnityEngine;
using System.Collections;
using System;
namespace Funds {
    public static class Funds {
        public static bool TransferFunds(IHasFunds from, IHasFunds to, int amount, bool force = false) {
            if (force) {
                from.ChangeFunds(-amount);
                to.ChangeFunds(amount);
                return true;
            }
            if (from.GetFunds() < amount) {
                return false;
            } else {
                from.ChangeFunds(-amount);
                to.ChangeFunds(amount);
                return true;
            }
        }

        public static bool TryToPurchase(IHasFunds purchaser, IHasCost purchase, bool force = false) {
            if (force) {
                purchaser.ChangeFunds(purchase.GetCost());
                return true;
            }
            if (purchaser.GetFunds() < purchase.GetCost()) {
                return false;
            } else {
                purchaser.ChangeFunds(-purchase.GetCost());
                return true;
            }
        }
    }
}

