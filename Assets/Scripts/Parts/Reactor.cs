using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Reactor : Part{

    public Tweakable averagePower;
    public Tweakable maxPower;

    public Reactor() : base(){
        partType = PartType.Reactor;
    }

    public Reactor(Part p) : base() {
        partType = PartType.Reactor;
        Reactor pp = (Reactor)p;
        for (int i = 0; i < pp.tweakables.Count; i++) {
            tweakables[i].Value = pp.tweakables[i].Value;
        }
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
        p.tier = 1;
        p.typeName = "Reactor";
        p.modelName = Constants.GetRandomReactorName();
        p.averagePower.Value = Random.Range(20, 100);
        p.maxPower.Value = Random.Range(100, 120);
        return p;
    }
}
