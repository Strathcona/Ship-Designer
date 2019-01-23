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
        FireControl p = new FireControl();
        p.sprite = SpriteLoader.GetPartSprite("defaultFireControlS");
        p.tier = Random.Range(1, 6);
        p.tracking.Value = Random.Range(1, 20);
        p.accuracy.Value = Random.Range(1, 20);
        p.size = PartSize.S;
        p.range.Value = Random.Range(1, 20);
        p.typeName = "Fire Control System";
        p.modelName = Constants.GetRandomFireControlModelName();
        Debug.Log(p.GetDescriptionString());
        Debug.Log(p.GetStatisticsString());
        return p;
    }
}
