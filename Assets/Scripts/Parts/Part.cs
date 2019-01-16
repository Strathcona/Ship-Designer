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
    public int unitTime = 1;
    public int unitPrice = 1;
    public bool inDevelopment = false;
    public bool inDelivery = false;
    public Timer timer;
    public PartType partType;
    public int minutesToDevelop = 6000;
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
        unitTime = p.unitTime;
        unitPrice = p.unitPrice;
        partType = p.partType;
        minutesToDevelop = p.minutesToDevelop;
        size = p.Size;
        tier = p.Tier;
        netPower = p.NetPower;
    }

    public virtual void CopyValuesFromPart(Part p) {
        typeName = p.typeName;
        modelName = p.modelName;
        manufacturerName = p.manufacturerName;
        quality = p.quality;
        unitTime = p.unitTime;
        unitPrice = p.unitPrice;
        partType = p.partType;
        minutesToDevelop = p.minutesToDevelop;
        size = p.Size;
        tier = p.Tier;
        netPower = p.NetPower;
    }

    public virtual string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }

    protected virtual void UpdateProperties() {
        unitPrice = 5 * Tier + Random.Range(1, 5);
        unitTime = 5 * Tier + Random.Range(1, 5);
    }
    public abstract string GetStatisticsString();
    public abstract void TweakableUpdate();
    protected abstract void InitializeTweakables();


}
