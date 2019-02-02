using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Reactor : Part{

    public Tweakable averagePower;
    public Tweakable maxPower;

    public Reactor() {
        partType = PartType.Reactor;
        InitializeTweakables();
    }

    protected override void InitializeTweakables() {
        averagePower = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Output");
        tweakables.Add(averagePower);

        maxPower = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Maximum Output");
        maxPower.unit = "%";
        tweakables.Add(maxPower);
    }

    public static Reactor GetRandomReactor() {
        Reactor p = new Reactor();
        p.sprite = SpriteLoader.GetPartSprite("defaultReactorS");
        p.tier = 1;
        p.Size = PartSize.S;
        p.typeName = "Reactor";
        p.modelName = Constants.GetRandomReactorName();
        p.averagePower.Value = Random.Range(20, 100);
        p.maxPower.Value = Random.Range(100, 120);
        return p;
    }

    public override Part Clone() {
        Reactor part = (Reactor)MemberwiseClone();
        part.manufacturer = manufacturer;
        foreach (Tweakable t in tweakables) {
            Tweakable newt = Tweakable.MakeTweakable(
                 part,
                 t.tweakableType,
                 part.TweakableUpdate,
                 t.tweakableName);
            newt.Value = t.Value;
            newt.dropdownLabels = new List<string>(t.dropdownLabels);
            part.tweakables.Add(newt);
        }
        return part;
    }
}
