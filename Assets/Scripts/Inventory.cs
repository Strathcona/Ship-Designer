using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory {
    public static Dictionary<Part, int> partInventory = new Dictionary<Part, int>();
    public static Dictionary<Part, int> partsOrdered = new Dictionary<Part, int>();
    public static List<PartOrder> partOrders = new List<PartOrder>();

    public static void OrderParts(PartOrder po) {
        if (po.part.inDevelopment) {
            Debug.LogError("Part was ordered while still in development");
        }
        partsOrdered.Add(po.part, po.units);
        partOrders.Add(po);
        po.part.inDelivery = true;
        var partpass = po.part;
        var numpass = po.units;
        po.part.timer = TimeManager.instance.SetTimer(po.time, delegate { RecieveParts(partpass, numpass); });
        Debug.Log("Ordered " + po.units.ToString() + " of " + po.part.GetDescriptionString());
    }

    public static void RecieveParts(Part p, int number) {
        if (partInventory.ContainsKey(p)) {
            partInventory[p] += number;
        } else {
            partInventory.Add(p, number);
        }
        Debug.Log("Recieved " + number.ToString() + " of " + p.GetDescriptionString());
    }
}
