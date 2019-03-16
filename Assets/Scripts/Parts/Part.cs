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
    public int weight = 1;
    public event Action<Part> OnPartChangeEvent;

    private PartSize size;
    public PartSize Size {
        get { return size; }
        set {
            size = value;
            OnPartChangeEvent?.Invoke(this);
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
    protected int netPower = 0;
    public int NetPower {
        get { return netPower; }
        set {
            netPower = value;
            OnPartChangeEvent?.Invoke(this);
            UpdateProperties();
        }
    }

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
            return Manufacturer.name + " " + ModelName + " " + DescriptionName;
        } else {
            return ModelName + " " + DescriptionName;
        }
    }

    protected virtual void UpdateProperties() {
        float weightedTweakableFactor = 0;
        foreach(Tweakable t in tweakables) {
            weightedTweakableFactor += (float)t.Value / t.MaxValue;
        }
        weightedTweakableFactor = weightedTweakableFactor / tweakables.Count;
        weight = Mathf.Max(1, Mathf.FloorToInt(Mathf.Pow(3 * weightedTweakableFactor, 2)*Constants.hardpointSizeFactor[Size]));
        designCost = Mathf.Max(1, Mathf.FloorToInt(Mathf.Pow(200 * weightedTweakableFactor, 1.5f))); ;
        cost = weight * Constants.hardpointSizeFactor[Size];

    }

    public virtual string GetStatisticsString() {
        string statisticsString = "Weight:"+weight.ToString();
        foreach(Tweakable t in tweakables) {
            statisticsString += " "+t.tweakableName + ":" + t.ValueString();
        }
        return statisticsString;
    }
    public virtual void TweakableUpdate() {
        OnPartChangeEvent?.Invoke(this);
        UpdateProperties();
    }

    protected abstract void InitializeTweakables();
    public abstract Part Clone();
}
