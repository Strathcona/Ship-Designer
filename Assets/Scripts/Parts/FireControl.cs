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
        InitializeTweakables();
    }

    protected override void InitializeTweakables() {
        tracking = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Tracking");

        accuracy = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Accuracy");
        range = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Range");
        tweakables.Add(tracking);
        tweakables.Add(accuracy);
        tweakables.Add(range);
        tracking.MaxCost = 20;
        accuracy.MaxDesignCost = 20;
        range.MaxWeight = 30;
    }

    public static FireControl GetRandomFireControl() {
        FireControl p = new FireControl();
        p.sprite = SpriteLoader.GetPartSprite("defaultFireControlS");
        p.tier = Random.Range(1, 6);
        p.tracking.Value = Random.Range(1, 20);
        p.accuracy.Value = Random.Range(1, 20);
        p.Size = PartSize.S;
        p.range.Value = Random.Range(1, 20);
        p.DescriptionName = "Fire Control System";
        p.ModelName = Constants.GetRandomFireControlModelName();
        Debug.Log(p.GetDescriptionString());
        Debug.Log(p.GetStatisticsString());
        return p;
    }

    public override Part Clone() {
        FireControl part = (FireControl) MemberwiseClone();
        List<Tweakable> newTweakables = new List<Tweakable>();
        part.Manufacturer = Manufacturer;
        foreach (Tweakable t in tweakables) {
            Tweakable newt = Tweakable.MakeTweakable(
                part,
                t.tweakableType,
                t.tweakableName);
            newt.Value = t.Value;
            newt.dropdownLabels = new List<string>(t.dropdownLabels);
            newTweakables.Add(newt);
        }
        part.tweakables = newTweakables;
        part.UpdateProperties();
        return part;
    }
}
