﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Engine : Part {
    public Tweakable agility;
    public Tweakable averageThrust;
    public Tweakable maxThrust;
    public Tweakable energyEfficiency;


    public Engine() : base(){
        partType = PartType.Engine;
    }

    public Engine(Part p) : base() {
        Engine e = (Engine)p;
        for (int i = 0; i < e.tweakables.Count; i++) {
            tweakables[i].Value = e.tweakables[i].Value;
        }
        partType = PartType.Engine;
        UpdateProperties();
    }

    protected override void InitializeTweakables() {
        agility = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Agility");

        averageThrust = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Thrust");

        maxThrust = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            100,
            100,
            100,
            150,
            "Maximum Thrust");
        maxThrust.unit = "%";

        energyEfficiency = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            100,
            100,
            100,
            125,
            "Energy Efficiency");
        energyEfficiency.unit = "%";

        tweakables.Add(agility);
        tweakables.Add(averageThrust);
        tweakables.Add(maxThrust);
        tweakables.Add(energyEfficiency);
    }

    public override void CopyValuesFromPart(Part p) {
        base.CopyValuesFromPart(p);
        Engine e = (Engine)p;
        for (int i = 0; i < e.tweakables.Count; i++) {
            tweakables[i].Value = e.tweakables[i].Value;
        }
        partType = PartType.Engine;
        UpdateProperties();
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
}
