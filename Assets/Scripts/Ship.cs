using System.Collections;
using GameConstructs;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Ship {
    public string shipName;
    public string className;

    public int minutesToDesign = 30000;
    public int minutesToBuild = 30000;

    public List<Hardpoint> hardpoints = new List<Hardpoint>();
    public List<Part> parts = new List<Part>();
    public Dictionary<PartType, Part> partsByPartType = new Dictionary<PartType, Part>();

    public void AddHardpoint(Hardpoint h) {
        h.onChange = GetPartsFromHardpoints;
        hardpoints.Add(h);
    }

    public void SetHardpoints(List<Hardpoint> _hardpoints) {
        hardpoints.Clear();
        hardpoints = _hardpoints;
        foreach(Hardpoint h in hardpoints) {
            h.onChange = GetPartsFromHardpoints;
        }
    }

    public void RemoveHardpoint(Hardpoint h) {
        hardpoints.Remove(h);
        GetPartsFromHardpoints();
    }

    public void GetPartsFromHardpoints() {
        parts.Clear();
        partsByPartType.Clear();
        foreach(Hardpoint h in hardpoints) {
            if(h.part!= null) {
                parts.Add(h.part);
                partsByPartType.Add(h.part.partType, h.part);
            }
        }
    }

    private void RecalculateStatisticsFromParts() {

    }

    public static Ship MakeUpARandomShip(){
        Ship s = new Ship();
        s.shipName = Constants.GetRandomShipName();
        s.className = Constants.GetRandomShipName();
        return s;
    }
}
