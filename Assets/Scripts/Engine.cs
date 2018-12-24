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

    public override string GetDescriptionString() {
        return manufacturerName + " " + partModelName + " " + partTypeName;
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

    public static Engine GetRandomShipEngine() {
        Engine s = new Engine();
        s.Tier = Random.Range(1, 6);
        s.manufacturerName = Constants.GetRandomCompanyName();
        s.Agility = Random.Range(1, 20);
        s.Thrust = Random.Range(1, 20);
        s.partTypeName = Constants.TierEngineNames[s.Tier] + " Engine";
        s.partModelName = Constants.GetRandomEngineModelName();
        s.numberOfPart = Random.Range(1, 8);
        Debug.Log(s.GetDescriptionString());
        Debug.Log(s.GetStatisticsString());
        return s;
    }
}
