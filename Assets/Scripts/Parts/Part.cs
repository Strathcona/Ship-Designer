using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;
using System;

[System.Serializable]
public abstract class Part: IDesigned, IHasCost {
    public string descriptionName = "";//A descriptive name of the part like 'Cold Fusion Turbine'
    public string DescriptionName {
        get { return descriptionName; }
        set {
            descriptionName = value;
            OnPartChangeEvent?.Invoke(this);
        }
    }
    private string modelName = ""; //Name of the make of the part like 'Devastator MkII'
    public string ModelName {
        get { return modelName; }
        set { modelName = value;
            OnPartChangeEvent?.Invoke(this);
        }
    }

    private Company manufacturer;//The maker of the Part
    public Company Manufacturer {
        get { return manufacturer; }
        set {
            manufacturer = value;
            OnManufactuerChange?.Invoke();
            OnPartChangeEvent?.Invoke(this);
        }
    }


    public float quality = 1.0f;
    public int unitTime = 1;
    public int unitPrice = 1;
    public PartType partType;
    public List<Tweakable> tweakables = new List<Tweakable>();
    public Sprite sprite;

    protected int maxWeight = 1;
    public int Weight = 1;

    public event Action<Part> OnPartChangeEvent;

    private PartSize size;
    public PartSize Size {
        get { return size; }
        set {
            size = value;
            OnPartChangeEvent?.Invoke(this);
            foreach(Tweakable t in tweakables) {
                t.ScaleFactor = Constants.sizeFactor[size];
            }
            UpdateProperties();
        }
    }
    protected int tier = 0;
    public int Tier {
        get { return tier; }
        set {
            tier = value;
            OnPartChangeEvent?.Invoke(this);
            UpdateProperties();
        }
    }

    protected int maxNetPower = 1;
    protected int netPower = 0;
    public int NetPower {
        get { return netPower; }
        set {
            netPower = value;
            OnPartChangeEvent?.Invoke(this);
            UpdateProperties();
        }
    }

    protected int maxCost = 1;
    protected int cost = 0;
    public int Cost{
        get{ return cost; }
        set{ cost = value;  }
    }

    protected bool isDesigned = false;
    public bool IsDesigned {
        get { return isDesigned; }
        set { isDesigned = value; }
    }

    protected int maxDesignCost = 1;
    protected int designCost = 0;
    public int DesignCost {
        get { return designCost; }
        set { designCost = value; }
    }


    protected float designProgress = 0;
    public float DesignProgress {
        get { return designProgress; }
        set {
            designProgress = value;
            OnDesignProgressEvent?.Invoke(this);
            if (designProgress > designCost) {
                isDesigned = true;
                designProgress = designCost;
                OnDesignChangeEvent?.Invoke(this);
            } else {
                OnDesignChangeEvent?.Invoke(this);
            }
        }
    }
    public event Action<IDesigned> OnDesignChangeEvent;
    public event Action<IDesigned> OnDesignProgressEvent;
    public event Action OnManufactuerChange;

    public virtual string GetDescriptionString() {
        if(Manufacturer != null) {
            return Manufacturer.name + " " + ModelName + " " + Constants.GetPartDescriptionName(this);
        } else {
            return ModelName + " " + Constants.GetPartDescriptionName(this);
        }
    }

    protected virtual void UpdateProperties() {
        Weight = 0;
        maxWeight = 0;
        cost = 0;
        maxCost = 0;
        designCost = 0;
        maxDesignCost = 0;
        netPower = 0;
        maxNetPower = 0;

        foreach(Tweakable t in tweakables) {
            if (t.automaticCalculation) {
                Weight += t.weight;
                maxWeight += t.maxWeight;

                cost += t.cost;
                maxCost += t.maxCost;

                designCost += t.designCost;
                maxDesignCost += t.maxDesignCost;

                netPower += t.netPower;
                maxNetPower += t.maxNetPower;
            }
        }
    }

    public virtual string GetStatisticsString() {
        string statisticsString = "Weight:"+Weight.ToString();
        foreach(Tweakable t in tweakables) {
            statisticsString += " "+t.tweakableName + ":" + t.ValueString();
        }
        return statisticsString;
    }

    public virtual string GetCostString() {
        return "Design Effort:"+DesignCost.ToString()+" Cost:"+Cost.ToString()+" credits" ;
    }

    public virtual Dictionary<string, float> GetNormalizedPerformanceValues() {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        dict.Add("Weight", (float)Weight/maxWeight);
        dict.Add("Cost", (float) Cost/maxCost);
        dict.Add("Design Cost", (float)DesignCost / maxDesignCost);
        dict.Add("Net Power", (float)NetPower / maxNetPower);


        return dict;
    }

    public virtual void TweakableUpdate(Tweakable t) {
        UpdateProperties();
        OnPartChangeEvent?.Invoke(this);
    }

    protected abstract void InitializeTweakables();
    public abstract Part Clone();
}
