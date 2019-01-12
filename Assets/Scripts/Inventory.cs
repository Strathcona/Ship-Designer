using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory {
    public static Dictionary<Part, int> partInventory = new Dictionary<Part, int>();
    public static Dictionary<Part, int> partsRecievable = new Dictionary<Part, int>();
    public static List<PartOrder> partOrders = new List<PartOrder>();

    public static void OrderParts(PartOrder po) {
        partsRecievable.Add(po.part, po.units);
        partOrders.Add(po);
    }
}
