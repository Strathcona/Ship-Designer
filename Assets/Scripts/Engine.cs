using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Engine : Part {
    public Tweakable agility;
    public Tweakable thrust;

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

        thrust = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Thrust");
        tweakables.Add(agility);
        tweakables.Add(thrust);
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

    public override string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }

    public override string GetStatisticsString() {
        return "Size: " + size + " Agility: " + agility.Value.ToString() + " Thrust: " + thrust.Value.ToString();
    }

    public override void TweakableUpdate() {

    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(agility.Value * 0.3f + thrust.Value * 0.4f));
    }

    public static Engine GetRandomEngine() {
        Engine s = new Engine();
        s.Tier = Random.Range(1, 6);
        s.manufacturerName = Constants.GetRandomCompanyName();
        s.agility.Value = Random.Range(1, 20);
        s.thrust.Value = Random.Range(1, 20);
        s.typeName = Constants.TierEngineNames[s.Tier] + " Engine";
        s.modelName = Constants.GetRandomEngineModelName();
        s.numberOfPart = Random.Range(1, 8);
        Debug.Log(s.GetDescriptionString());
        Debug.Log(s.GetStatisticsString());
        return s;
    }
}
