using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Reactor : Part{

    public Tweakable averagePower;
    public Tweakable maxPower;

    public Reactor(): base() {
        partType = PartType.Reactor;
    }

    protected override void InitializeTweakables() {
        averagePower = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            50,
            50,
            1,
            100,
            "Baseline Output");
        tweakables.Add(averagePower);

        maxPower = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            100,
            110,
            110,
            120,
            "Max Output");
        maxPower.unit = "%";
        tweakables.Add(maxPower);
    }

    public static Reactor GetRandomReactor() {
        Reactor p = new Reactor();
        p.sprite = SpriteLoader.GetPartSprite("defaultReactorS");
        p.tier = 1;
        p.size = PartSize.S;
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
                t.Value,
                t.defaultIntValue,
                t.minIntValue,
                t.maxIntValue,
                t.tweakableName);
            newt.dropdownLabels = new List<string>(t.dropdownLabels);
            part.tweakables.Add(newt);
        }
        return part;
    }
}
