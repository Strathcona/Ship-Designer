using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Engine : Part {
    private int agility;
    public int Agility {
        get { return agility; }
        set {
            agility = value;
            UpdateProperties();
        }
    }
    private int thrust;
    public int Thrust {
        get { return thrust; }
        set {
            thrust = value;
            UpdateProperties();
        }
    }

    public Engine() {
        partType = PartType.Engine;
    }

    public Engine(Part p) : base() {
        Engine e = (Engine)p;
        thrust = e.Thrust;
        agility = e.Agility;
        partType = PartType.Engine;
        UpdateProperties();
    }

    public override void CopyValuesFromPart(Part p) {
        base.CopyValuesFromPart(p);
        Engine e = (Engine)p;
        thrust = e.Thrust;
        agility = e.Agility;
        partType = PartType.Engine;
        UpdateProperties();
    }

    public override string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }
    public override string GetStatisticsString() {
        return "Size: " + size + " Agility: " + agility + " Thrust: " + thrust;
    }
    public override string GetPartString() {
        return "ShipEngine";
    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(agility * 0.3f + thrust * 0.4f));
    }

    public static Engine GetRandomEngine() {
        Engine s = new Engine();
        s.Tier = Random.Range(1, 6);
        s.manufacturerName = Constants.GetRandomCompanyName();
        s.Agility = Random.Range(1, 20);
        s.Thrust = Random.Range(1, 20);
        s.typeName = Constants.TierEngineNames[s.Tier] + " Engine";
        s.modelName = Constants.GetRandomEngineModelName();
        s.numberOfPart = Random.Range(1, 8);
        Debug.Log(s.GetDescriptionString());
        Debug.Log(s.GetStatisticsString());
        return s;
    }
}
