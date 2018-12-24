using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public abstract class Part {
    public string partTypeName = ""; //A descriptive name of the part like 'Cold Fusion Turbine'
    public string partModelName = ""; //Name of the make of the part like 'Devastator'
    public string manufacturerName = ""; //Name of the maker of the Part
    public float quality = 1.0f;
    public int timeCost = 0;
    public int creditCost = 0;
    public PartType partType;
    protected int size = 1;
    public int Size {
        get { return size; }
    }
    protected int tier = 0;
    public int Tier {
        get { return tier; }
        set {
            tier = value;
            UpdateProperties();
        }
    }
    protected int netPower = 0;
    public int NetPower {
        get { return netPower; }
        set {
            netPower = value;
            UpdateProperties();
        }
    }
    public int numberOfPart = 1;


    public int GetTotalSize() {
        return size * numberOfPart;
    }

    protected virtual void UpdateProperties() {}//could be called by children
    public abstract string GetDescriptionString();
    public abstract string GetStatisticsString();
    public abstract string GetPartString();
}
