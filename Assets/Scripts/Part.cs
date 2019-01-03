using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public abstract class Part {
    public string typeName = ""; //A descriptive name of the part like 'Cold Fusion Turbine'
    public string modelName = ""; //Name of the make of the part like 'Devastator'
    public string manufacturerName = ""; //Name of the maker of the Part
    public float quality = 1.0f;
    public int timeCost = 0;
    public int creditCost = 0;
    public int complexityCost = 0;
    public PartType partType;
    public int ticksToDesign = 100;
    public List<Tweakable> tweakables = new List<Tweakable>();
    protected int size = 1;
    public int Size {
        get { return size; }
        set {
            size = value;
            UpdateProperties();
        }
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

    public Part() {
        InitializeTweakables();
    }

    public Part(Part p) {
        InitializeTweakables();
        typeName = p.typeName;
        modelName = p.modelName;
        manufacturerName = p.manufacturerName;
        quality = p.quality;
        timeCost = p.timeCost;
        creditCost = p.creditCost;
        partType = p.partType;
        ticksToDesign = p.ticksToDesign;
        size = p.Size;
        tier = p.Tier;
        netPower = p.NetPower;
    }

    public virtual void CopyValuesFromPart(Part p) {
        typeName = p.typeName;
        modelName = p.modelName;
        manufacturerName = p.manufacturerName;
        quality = p.quality;
        timeCost = p.timeCost;
        creditCost = p.creditCost;
        partType = p.partType;
        ticksToDesign = p.ticksToDesign;
        size = p.Size;
        tier = p.Tier;
        netPower = p.NetPower;
    }

    public virtual string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }

    protected virtual void UpdateProperties() {
        complexityCost = Mathf.FloorToInt(Mathf.Pow(2, tier));
    }
    public abstract string GetStatisticsString();
    public abstract void TweakableUpdate();
    protected abstract void InitializeTweakables();


}
