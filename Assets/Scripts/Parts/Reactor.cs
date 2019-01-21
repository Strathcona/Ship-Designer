using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Reactor : Part{

    public Tweakable power;

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
        power = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Power Output");
        tweakables.Add(power);
    }


    public override string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }
    public override string GetStatisticsString() {
        return "Size: " + size.ToString() + " Output: " + netPower.ToString();
    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(0.1f * Mathf.Pow(netPower, 1.3f)));
    }

    public override void TweakableUpdate() {
        netPower = power.Value;
    }

    public static Reactor GetRandomReactor() {
        Reactor p = new Reactor();
        p.tier = 1;
        p.typeName = "Reactor";
        p.modelName = Constants.GetRandomReactorName();
        p.power.Value = Random.Range(20, 100);
        return p;
    }
}
