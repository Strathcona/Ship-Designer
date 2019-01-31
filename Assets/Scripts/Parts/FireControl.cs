using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class FireControl : Part{
    public Tweakable tracking;
    public Tweakable accuracy;
    public Tweakable range;

    public FireControl() : base(){
        partType = PartType.FireControl;
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

    public override Part Clone() {
        FireControl part = (FireControl)MemberwiseClone();
        part.manufacturer = manufacturer;
        foreach (Tweakable t in tweakables) {
            Tweakable newt = Tweakable.MakeTweakable(
                part,
                t.tweakableType,
                part.TweakableUpdate,
                t.Value,
                t.DefaultValue,
                t.MinValue,
                t.MaxValue,
                t.tweakableName);
            newt.dropdownLabels = new List<string>(t.dropdownLabels);
            part.tweakables.Add(newt);
        }
        return part;
    }
}
