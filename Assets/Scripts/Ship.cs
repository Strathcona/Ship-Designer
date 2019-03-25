using System.Collections;
using GameConstructs;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Ship {
    public string shipName;
    public string className;
    public ShipType classificaiton;
    public int crew = 0;
    public int tonnage = 0;
    public bool inDevelopment;

    //public int powerSupply = 0;
    //public int powerConsumption = 0;
    //public int powerMaxConsumption = 0;
    //public int thrust;
    //public int maxThrust;
    //public int agility;
    //public int alphaDamage;
    //public int averageDamage;

    public int minutesToDevelop = 30000;
    public int minutesToBuild = 30000;

    public List<Hardpoint> hardpoints = new List<Hardpoint>();
    public List<Part> parts = new List<Part>();
    public Dictionary<PartType, List<Part>> partsByPartType = new Dictionary<PartType, List<Part>>();

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
        GetPartsFromHardpoints();
        RecalculateShipClassification();
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
                if (partsByPartType.ContainsKey(h.part.partType)) {
                    partsByPartType[h.part.partType].Add(h.part);
                } else {
                    partsByPartType.Add(h.part.partType, new List<Part>() { h.part });
                }

            }
        }
        RecalculateTonnage();
    }

    private void RecalculateTonnage() {
        tonnage = 0;
        foreach(Part p in parts) {
            tonnage += p.Weight;
        }
    }

    private void RecalculateShipClassification() {
        int xs = hardpoints.FindAll(h => h.allowableSize == PartSize.XS).Count;
        int s = hardpoints.FindAll(h => h.allowableSize == PartSize.S).Count;
        int m = hardpoints.FindAll(h => h.allowableSize == PartSize.M).Count;
        int l = hardpoints.FindAll(h => h.allowableSize == PartSize.L).Count;
        int xl = hardpoints.FindAll(h => h.allowableSize == PartSize.XL).Count;
        Debug.Log(xs + " " + s + " " + m + " " + l + " " + xl);
        if (xs == 0 && s == 0 && m == 0 && l == 0 && xl == 0) {
            classificaiton = ShipType.None;
        }
        if (s == 0 && m == 0 && l == 0 && xl == 0) {
            //only xs
            if (xs > 15) {
                if (hardpoints.FindAll(h => h.allowableType == PartType.Weapon).Count > 0) {
                    //got a bunch of xs slots and weapons
                    classificaiton = ShipType.Gunboat;
                } else {
                    //got a bunch of xs slots but no weapons
                    classificaiton = ShipType.Utility;
                }
            } else {
                //only a few slots 
                if (hardpoints.FindAll(h => h.allowableType == PartType.Weapon).Count > 0) {
                    classificaiton = ShipType.Fighter;
                } else {
                    classificaiton = ShipType.Patrol;
                }
            }
        } else if (m == 0 && l == 0 && xl == 0) {
            if (xs + s > 15) {
                classificaiton = ShipType.LightCruiser;
            } else {
                classificaiton = ShipType.Destroyer;
            }
        } else if (l == 0 && xl == 0) {
            if (xs + s + m > 40) {
                classificaiton = ShipType.Battlecruiser;
            } else if (xs + s + m > 20) {
                classificaiton = ShipType.HeavyCruiser;
            } else {
                classificaiton = ShipType.LightCruiser;
            }
        } else if (xs + s + m + l + xl > 40) {
            classificaiton = ShipType.Battleship;
        } else {
            classificaiton = ShipType.Battlecruiser;
        }

        if (classificaiton == ShipType.Fighter) {
            crew = 1;
        } else {
            foreach (Hardpoint h in hardpoints) {
                crew += Constants.sizeFactor[h.allowableSize];
            }
            crew = Mathf.CeilToInt(crew * 1.1f);
        }
    }

    public static Ship MakeUpARandomShip(){
        Ship s = new Ship();
        s.shipName = Constants.GetRandomShipName();
        s.className = Constants.GetRandomShipName();
        return s;
    }
}
