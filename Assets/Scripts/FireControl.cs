﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class FireControl : Part{
    private int tracking;
    public int Tracking {
        get { return tracking; }
        set {
            tracking = value;
            UpdateProperties();
        }
    }
    private int accuracy;
    public int Accuracy {
        get { return accuracy; }
        set {
            accuracy = value;
            UpdateProperties();
        }
    }
    private int range;
    public int Range {
        get { return range; }
        set {
            range = value;
            UpdateProperties();
        }
    }

    public FireControl() {
        partType = PartType.FireControl;
    }


    public FireControl(Part p) : base() {
        FireControl f = (FireControl)p;
        tracking = f.Tracking;
        accuracy = f.Accuracy;
        range = f.Range;
        partType = PartType.FireControl;
        UpdateProperties();
    }

    public override void CopyValuesFromPart(Part p) {
        base.CopyValuesFromPart(p);
        FireControl f = (FireControl)p;
        tracking = f.Tracking;
        accuracy = f.Accuracy;
        range = f.Range;
        partType = PartType.FireControl;
        UpdateProperties();
    }

    public override string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }
    public override string GetStatisticsString() {
        return "Size: "+size+" Tracking: " + tracking + " Accuracy: " + accuracy + " Effective Range: " + range;
    }

    public override string GetPartString() {
        return "FireControl";
    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(0.1f * (tracking + accuracy + range) / Constants.TierFireControlAccuracy[tier]));
    }

    public static FireControl GetRandomFireControl() {
        FireControl f = new FireControl();
        f.tier = Random.Range(1, 6);
        f.manufacturerName = Constants.GetRandomCompanyName();
        f.tracking = Random.Range(1, 20);
        f.accuracy = Random.Range(1, 20);
        f.range = Random.Range(1, 20);
        f.typeName = "Fire Control System";
        f.modelName = Constants.GetRandomFireControlModelName();
        f.numberOfPart = Random.Range(1, 3);
        Debug.Log(f.GetDescriptionString());
        Debug.Log(f.GetStatisticsString());
        return f;
    }
}
