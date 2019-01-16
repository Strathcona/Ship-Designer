using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class PartLibrary {
    private static List<Part> partsInDevelopment = new List<Part>();
    private static List<Part> parts = new List<Part>();
    private static List<Part> obsoleteParts = new List<Part>();

    static PartLibrary() {
        parts = new List<Part>() {
            Weapon.GetRandomLaser(),
            Sensor.GetRandomSensor(),
            FireControl.GetRandomFireControl(),
            Engine.GetRandomEngine(),
            PowerPlant.GetRandomPowerPlant()
        };
    }

    public static void AddPartToDevelopment(Part p) {
        partsInDevelopment.Add(p);
        p.inDevelopment = true;
        var pass = p;
        Timer t = TimeManager.instance.SetTimer(p.minutesToDevelop, delegate { CompleteDevelopmentOfPart(pass); });
        p.timer = t;
    }

    public static void CompleteDevelopmentOfPart(Part p) {
        Debug.Log("Part complete development " + p.GetDescriptionString());
        p.inDevelopment = false;
        partsInDevelopment.Remove(p);
        parts.Add(p);
    }

    public static List<Part> GetParts() {
        return new List<Part>(parts);
    }

    public static List<Part> GetUndevelopedParts() {
        return new List<Part>(partsInDevelopment);
    }

    public static bool ObsoletePart(Part p) {
        if (parts.Contains(p)) {
            parts.Remove(p);
            if (obsoleteParts.Contains(p)) {
                return true;
            } else {
                obsoleteParts.Add(p);
                return true;
            }
        } else {
            return false;
        }
    }


}
