using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class PartLibrary {
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

    public static bool AddPartToLibrary(Part p) {
        if (parts.Contains(p)) {
            return false;
        } else {
            parts.Add(p);
            return true;
        }
    }

    public static List<Part> GetParts() {
        return new List<Part>(parts);
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
