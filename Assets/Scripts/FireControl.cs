using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class FireControl : Part{
    public Tweakable tracking;
    public Tweakable accuracy;
    public Tweakable range;

    public FireControl() {
        partType = PartType.FireControl;
    }


    public FireControl(Part p) : base() {
        FireControl f = (FireControl)p;
        for (int i = 0; i < f.tweakables.Count; i++) {
            tweakables[i].Value = f.tweakables[i].Value;
        }
        partType = PartType.FireControl;
        UpdateProperties();
    }

    protected override void InitializeTweakables() {
        tracking = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Tracking");

        accuracy = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Accuracy");
        range = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Range");
        tweakables.Add(tracking);
        tweakables.Add(accuracy);
        tweakables.Add(range);
    }

    public override void CopyValuesFromPart(Part p) {
        base.CopyValuesFromPart(p);
        FireControl f = (FireControl)p;
        for (int i = 0; i < f.tweakables.Count; i++) {
            tweakables[i].Value = f.tweakables[i].Value;
        }
        partType = PartType.FireControl;
        UpdateProperties();
    }

    public override string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }
    public override string GetStatisticsString() {
        return "Size: "+size+" Tracking: " + tracking.Value.ToString() + " Accuracy: " + accuracy.Value.ToString() + " Effective Range: " + range.Value.ToString();
    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(0.1f * (tracking.Value + accuracy.Value + range.Value) / Constants.TierFireControlAccuracy[tier]));
    }

    public override void TweakableUpdate() {

    }

    public static FireControl GetRandomFireControl() {
        FireControl f = new FireControl();
        f.tier = Random.Range(1, 6);
        f.manufacturerName = Constants.GetRandomCompanyName();
        f.tracking.Value = Random.Range(1, 20);
        f.accuracy.Value = Random.Range(1, 20);
        f.range.Value = Random.Range(1, 20);
        f.typeName = "Fire Control System";
        f.modelName = Constants.GetRandomFireControlModelName();
        f.numberOfPart = Random.Range(1, 3);
        Debug.Log(f.GetDescriptionString());
        Debug.Log(f.GetStatisticsString());
        return f;
    }
}
