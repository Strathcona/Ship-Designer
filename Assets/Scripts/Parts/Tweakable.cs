using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameConstructs;

[System.Serializable]
public class Tweakable {
    public Part part;
    public TweakableType tweakableType;
    public event Action<Tweakable> OnTweakableChangedEvent; //update the part when this tweakable changes
    public string unit = "";
    public string tweakableName;

    private int value;
    public int Value {
        get { return value; }
        set {
            this.value = value;
            OnValueChanged();
        }
    }
    public float NormalizedValue {
        get {
            if (!ReverseScaling) {
                if(MaxValue == 0) {
                    return 0.0f;
                }
                return (float) value / MaxValue;
            } else {
                return  1 - ((float) value / MaxValue);
            }
        }
    }

    public bool automaticCalculation = true;

    public int maxWeight = 10;
    public int maxCost = 10;
    public int maxDesignCost = 10;
    public int maxNetPower = 10;

    public int weight { get { return Mathf.Max(1, Mathf.RoundToInt(NormalizedValue * maxWeight)); } }
    public int cost { get { return Mathf.Max(1, Mathf.RoundToInt(NormalizedValue * maxCost)); } }
    public int designCost { get { return Mathf.Max(1, Mathf.RoundToInt(NormalizedValue * maxDesignCost)); } }
    public int netPower { get { return Mathf.RoundToInt(NormalizedValue * maxNetPower); } }


    private int scaleFactor = 1;
    public int ScaleFactor {
        get { return scaleFactor; }
        set {
            scaleFactor = value;
            OnValueChanged();
        }
    }
    public bool scaleMin = true; //do we scale the min based on the scale factor?
    public bool scaleMax = true; //do we scale the max based on the scale factor?

    private int baseMinValue { get { return ResearchManager.instance.GetResearchValue(GetResearchManagerKey("MinValue")); } }
    private int baseMaxValue { get { return ResearchManager.instance.GetResearchValue(GetResearchManagerKey("MaxValue")); } }
    public int MinValue { get { if (scaleMin) { return baseMinValue * scaleFactor; } else { return baseMinValue; } } }
    public int MaxValue { get { if (scaleMax) { return baseMaxValue * scaleFactor; } else { return baseMaxValue; } } }

    public List<string> dropdownLabels = new List<string>();
    private bool reverseScaling = false;
    public bool ReverseScaling {
        get { return reverseScaling; }
        set { if(value == true) {
                reverseScaling = true;
                Value = MaxValue;
            } else {
                reverseScaling = false;
                Value = MinValue;
            }
        }
    }

    public string ValueString() {
        return Value.ToString() + unit;
    }

    private void OnValueChanged() {
        if(OnTweakableChangedEvent != null) {
            OnTweakableChangedEvent.Invoke(this);
        } else {
            Debug.LogError("Tried to UpdatePart when action was not set");
        }
    }

    Tweakable(string _tweakbleName) {
        tweakableName = _tweakbleName;
    }

    public static Tweakable MakeTweakable(
        Part _part,
        TweakableType _tweakableType,
        string _tweakableName){
        Tweakable t = new Tweakable(_tweakableName);
        t.part = _part;
        t.tweakableType = _tweakableType;
        t.value = t.MinValue;
        t.OnTweakableChangedEvent += t.part.TweakableUpdate;
        return t;
    }

    private string GetResearchManagerKey(string fieldName) {
        return Constants.PartTypeString[part.partType] + " " + tweakableName + " " + fieldName;
    }
}
