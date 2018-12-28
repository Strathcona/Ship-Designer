using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class PowerPlant : Part{

    public PowerPlant() {
        partType = PartType.PowerPlant;
    }

    public PowerPlant(Part p) : base() {
        partType = PartType.PowerPlant;
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

    public static PowerPlant GetRandomPowerPlant() {
        PowerPlant p = new PowerPlant();
        p.tier = 1;
        p.typeName = "Reactor";
        p.modelName = Constants.GetRandomPowerPlantModelName();
        p.manufacturerName = Constants.GetRandomCompanyName();
        p.netPower = Random.Range(20, 100);
        return p;
    }
}
