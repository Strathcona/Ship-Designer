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


    public Engine() {
        partType = PartType.Engine;
        InitializeTweakables();
    }

    protected override void InitializeTweakables() {
        agility = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Agility");
        averageThrust = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Thrust");

        maxThrust = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Maximum Thrust");
        maxThrust.unit = "%";

        energyEfficiency = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Energy Efficiency");
        energyEfficiency.unit = "%";

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
        p.size = PartSize.S;
        p.averageThrust.Value = Random.Range(1, 20);
        p.typeName = Constants.TierEngineNames[p.Tier] + " Engine";
        p.modelName = Constants.GetRandomEngineModelName();
        Debug.Log(p.GetDescriptionString());
        Debug.Log(p.GetStatisticsString());
        return p;
    }

    public override Part Clone() {
        Engine part = (Engine)MemberwiseClone();
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
