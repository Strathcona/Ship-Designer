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

    private int value;
    public int Value {
        get { return value; }
        set {
            this.value = value;
            OnValueChanged();
        }
    }
    private int scaleFactor = 1;
    public int ScaleFactor {
        get { return scaleFactor; }
        set {
            scaleFactor = value;
            Debug.Log(tweakableName + " scale factor changed to " + scaleFactor);
            OnValueChanged();
        }
    }
    private int baseMinValue { get { return ResearchManager.instance.GetResearchValue(GetResearchManagerKey("MinValue")); } }
    private int baseMaxValue { get { return ResearchManager.instance.GetResearchValue(GetResearchManagerKey("MaxValue")); } }
    public int MinValue { get { return baseMinValue * scaleFactor; } }
    public int MaxValue { get { return baseMaxValue * scaleFactor; } }
    
    public List<string> dropdownLabels = new List<string>();
    public string tweakableName;
    private bool reverseScaling;
    public bool ReverseScaling {
        get { return reverseScaling; }
        set { if(value == true) {
                reverseScaling = true;
                Value = MaxValue;
                OnValueChanged();
            } else {
                reverseScaling = false;
                Value = MinValue;
                OnValueChanged();
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

    public float GetDesignCostFactor() {
        if (ReverseScaling) {
            return (float)MinValue / Value;
        } else {
            return (float)Value / MaxValue;
        }
    }

    private string GetResearchManagerKey(string fieldName) {
        return Constants.PartTypeString[part.partType] + " " + tweakableName + " " + fieldName;
    }
}
