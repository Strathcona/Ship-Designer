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

    private int value;
    public int Value {
        get { return value; }
        set {
            this.value = value;
            OnValueChanged();
        }
    }
    public int defaultIntValue = 0;
    public int minIntValue = 0;
    public int maxIntValue = 100;
    public List<string> dropdownLabels = new List<string>();
    public string tweakableName;

    private Tweakable() {

    }

    private void OnValueChanged() {
        if(UpdatePart != null) {
            Debug.Log("Updating Part");
            UpdatePart();
        } else {
            Debug.LogError("Tried to UpdatePart when action was not set");
        }
    }

    public static Tweakable MakeTweakable(
        Part _part,
        TweakableType _tweakableType,
        Action _updatePart,
        int _intValue,
        int _defaultIntValue,
        int _minIntValue,
        int _maxIntValue,
        string _tweakableName){

        Tweakable t = new Tweakable();
        t.part = _part;
        t.UpdatePart = _updatePart;
        t.tweakableType = _tweakableType;
        t.value = _intValue;
        t.defaultIntValue = _defaultIntValue;
        t.minIntValue = _minIntValue;
        t.maxIntValue = _maxIntValue;
        t.tweakableName = _tweakableName;
        return t;
    }
}
