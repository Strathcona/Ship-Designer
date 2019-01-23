using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public abstract class Part {
    public string typeName = ""; //A descriptive name of the part like 'Cold Fusion Turbine'
    public string modelName = ""; //Name of the make of the part like 'Devastator'
    public Company manufacturer; //Name of the maker of the Part
    public float quality = 1.0f;
    public int unitTime = 1;
    public int unitPrice = 1;
    public bool inDevelopment = false;
    public Timer timer;
    public PartType partType;
    public int minutesToDevelop = 6000;
    public List<Tweakable> tweakables = new List<Tweakable>();
    public Sprite sprite;

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

    public Part() {
        InitializeTweakables();
    }

    public Part(Part p) {
        InitializeTweakables();
        typeName = p.typeName;
        modelName = p.modelName;
        manufacturer = p.manufacturer;
        sprite = p.sprite;
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
        manufacturer = p.manufacturer;
        sprite = p.sprite;
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
        if(manufacturer != null) {
            return manufacturer.name + " " + modelName + " " + typeName;
        } else {
            return modelName + " " + typeName;
        }
    }

    protected virtual void UpdateProperties() {
        float weightedTweakableFactor = 0;
        foreach(Tweakable t in tweakables) {
            weightedTweakableFactor += (float)t.Value / t.maxIntValue;
        }
        weightedTweakableFactor = weightedTweakableFactor / tweakables.Count;
        size = Mathf.Max(1, Mathf.FloorToInt(Mathf.Pow(3 * weightedTweakableFactor, 2)));
        minutesToDevelop = Mathf.Max(1, Mathf.FloorToInt(Mathf.Pow(200 * weightedTweakableFactor, 1.5f))); ;
        unitPrice = size * 10;
        unitTime = minutesToDevelop / 10;
    }

    public virtual string GetStatisticsString() {
        string statisticsString = "Size:"+Size.ToString();
        foreach(Tweakable t in tweakables) {
            statisticsString += " "+t.tweakableName + ":" + t.ValueString();
        }
        return statisticsString;
    }
    public virtual void TweakableUpdate() {
        UpdateProperties();
    }
    protected abstract void InitializeTweakables();


}
