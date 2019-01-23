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

    public static FireControl GetRandomFireControl() {
        FireControl f = new FireControl();
        f.sprite = SpriteLoader.GetPartSprite("defaultFireControlS");
        f.tier = Random.Range(1, 6);
        f.tracking.Value = Random.Range(1, 20);
        f.accuracy.Value = Random.Range(1, 20);
        f.range.Value = Random.Range(1, 20);
        f.typeName = "Fire Control System";
        f.modelName = Constants.GetRandomFireControlModelName();
        Debug.Log(f.GetDescriptionString());
        Debug.Log(f.GetStatisticsString());
        return f;
    }
}
