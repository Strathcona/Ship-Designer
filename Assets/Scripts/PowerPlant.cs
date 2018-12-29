using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class PowerPlant : Part{

    public Tweakable power;

    public PowerPlant() : base(){
        partType = PartType.PowerPlant;
    }

    public PowerPlant(Part p) : base() {
        partType = PartType.PowerPlant;
        PowerPlant pp = (PowerPlant)p;
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
        string number = (numberOfPart + " x ");
        return number + manufacturerName + " " + modelName + " " + typeName;
    }
    public override string GetStatisticsString() {
        return "Size: " + size.ToString() + " Output: " + netPower.ToString();
    }
    public override string GetPartString() {
        return "PowerPlant";
    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(0.1f * Mathf.Pow(netPower, 1.3f)));
    }

    public override void TweakableUpdate() {
        netPower = power.Value;
    }

    public static PowerPlant GetRandomPowerPlant() {
        PowerPlant p = new PowerPlant();
        p.tier = 1;
        p.typeName = "Reactor";
        p.modelName = Constants.GetRandomPowerPlantModelName();
        p.manufacturerName = Constants.GetRandomCompanyName();
        p.power.Value = Random.Range(20, 100);
        return p;
    }
}
