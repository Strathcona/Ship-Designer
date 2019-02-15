using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLibrary : MonoBehaviour
{
    private static List<Ship> shipsInDevelopment = new List<Ship>();
    private static List<Ship> shipDesigns = new List<Ship>();


    public static void AddShipToDevelopment(Ship s) {
        shipsInDevelopment.Add(s);
        s.inDevelopment = true;
        var pass = s;
        TimeTrigger t = TimeManager.instance.SetTimeTrigger(s.minutesToDevelop, delegate { CompleteDevelopmentOfPart(pass); });
    }

    public static void CompleteDevelopmentOfPart(Ship s) {
        Debug.Log("Ship complete development " + s.className);
        s.inDevelopment = false;
        shipsInDevelopment.Remove(s);
        shipDesigns.Add(s);
    }

    public static List<Ship> GetShips() {
        return new List<Ship>(shipDesigns);
    }

    public static List<Ship> GetUndevelopedShips() {
        return new List<Ship>(shipsInDevelopment);
    }

}
