using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameConstructs;

[System.Serializable]
public class Sensor : Part{
    public Tweakable range;
    public Tweakable resolution;
    public SensorType sensorType;

    public Sensor() {
        partType = PartType.Sensor;
        InitializeTweakables();
    }

    protected override void InitializeTweakables() {
        range = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Range");
        resolution = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Resolution");

        tweakables.Add(range);
        tweakables.Add(resolution);
    }

    public static Sensor GetRandomSensor() {
        Sensor p = new Sensor();
        p.sprite = SpriteLoader.GetPartSprite("defaultSensorS");
        p.Tier = 1;
        p.Size = PartSize.S;
        p.sensorType = SensorType.LowEnergy;
        p.DescriptionName = "Low Energy Sensor";
        p.ModelName = StringLoader.GetAString("sensorNames");
        p.range.Value = UnityEngine.Random.Range(2, 20);
        p.resolution.Value = UnityEngine.Random.Range(2, 20);
        return p;

    }

    public override Part Clone() {
        Sensor part = (Sensor)MemberwiseClone();
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
