using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Engine : Part {
    public Tweakable agility;
    public Tweakable averageThrust;
    public Tweakable maxThrust;
    public Tweakable energyEfficiency;

    public override int NetPower {
        get { return Mathf.CeilToInt(netPower / ((float)energyEfficiency.Value / 100)); }
        set {
            netPower = value;
            base.OnPartChanged();
        }
    }


    public Engine() {
        partType = PartType.Engine;
        InitializeTweakables();
    }

    protected override void InitializeTweakables() {
        agility = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Agility");

        averageThrust = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Thrust");

        maxThrust = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Maximum Thrust");
        maxThrust.unit = "%";

        energyEfficiency = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Energy Efficiency");
        energyEfficiency.unit = "%";

        maxThrust.scaleMin = false;
        maxThrust.MaxCost = 20;
        energyEfficiency.MaxDesignCost = 15;
        energyEfficiency.MaxCost = 15;
        energyEfficiency.MaxNetPower = 1;
        energyEfficiency.scaleMin = false;
        tweakables.Add(agility);
        tweakables.Add(averageThrust);
        tweakables.Add(maxThrust);
        tweakables.Add(energyEfficiency);
    }

    public static Engine GetRandomEngine() {
        Engine p = new Engine();
        p.sprite = SpriteLoader.GetPartSprite("defaultEngineS");
        p.Tier = Random.Range(1, 6);
        p.agility.Value = Random.Range(1, 20);
        p.Size = PartSize.S;
        p.averageThrust.Value = Random.Range(1, 20);
        p.DescriptionName = Constants.GetPartDescriptionName(p);
        p.ModelName = Constants.GetRandomEngineModelName();
        Debug.Log(p.GetDescriptionString());
        Debug.Log(p.GetStatisticsString());
        return p;
    }

    public override Part Clone() {
        Engine part = (Engine)MemberwiseClone();
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
