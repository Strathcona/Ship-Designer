using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameConstructs;

[System.Serializable]
public class Tweakable {
    public Part part;
    public TweakableType tweakableType;
    public Action UpdatePart; //update the part when it's tweaked
    public string unit = "";

    private int value;
    public int Value {
        get { return value; }
        set {
            this.value = value;
            OnValueChanged();
        }
    }
    public int MinValue { get { return ResearchManager.instance.GetResearchValue(GetResearchManagerKey("MinValue")); } }
    public int MaxValue { get { return ResearchManager.instance.GetResearchValue(GetResearchManagerKey("MaxValue")); } }
    public List<string> dropdownLabels = new List<string>();
    public string tweakableName;

    public string ValueString() {
        return Value.ToString() + unit;
    }

    private void OnValueChanged() {
        if(UpdatePart != null) {
            UpdatePart();
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
        Action _updatePart,
        string _tweakableName){
        Tweakable t = new Tweakable(_tweakableName);
        t.part = _part;
        t.UpdatePart = _updatePart;
        t.tweakableType = _tweakableType;
        t.value = t.MinValue;
        return t;
    }

    private string GetResearchManagerKey(string fieldName) {
        return Constants.PartTypeString[part.partType] + " " + tweakableName + " " + fieldName;
    }
}
