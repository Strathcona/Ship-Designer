using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public abstract class Part {
    public string typeName = ""; //A descriptive name of the part like 'Cold Fusion Turbine'
    public string modelName = ""; //Name of the make of the part like 'Devastator'
    public Company manufacturer; //The maker of the Part
    public float quality = 1.0f;
    public int unitTime = 1;
    public int unitPrice = 1;
    public bool inDevelopment = false;
    public PartType partType;
    public int minutesToDevelop = 6000;
    public List<Tweakable> tweakables = new List<Tweakable>();
    public Sprite sprite;
    public PartSize size;
    public int weight = 1;

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
            weightedTweakableFactor += (float)t.Value / t.MaxValue;
        }
        weightedTweakableFactor = weightedTweakableFactor / tweakables.Count;
        weight = Mathf.Max(1, Mathf.FloorToInt(Mathf.Pow(3 * weightedTweakableFactor, 2)));
        minutesToDevelop = Mathf.Max(1, Mathf.FloorToInt(Mathf.Pow(200 * weightedTweakableFactor, 1.5f))); ;
        unitPrice = weight * 10;
        unitTime = minutesToDevelop / 10;
    }

    public virtual string GetStatisticsString() {
        string statisticsString = "Weight:"+weight.ToString();
        foreach(Tweakable t in tweakables) {
            statisticsString += " "+t.tweakableName + ":" + t.ValueString();
        }
        return statisticsString;
    }
    public virtual void TweakableUpdate() {
        UpdateProperties();
    }

    protected abstract void InitializeTweakables();
    public abstract Part Clone();

}
