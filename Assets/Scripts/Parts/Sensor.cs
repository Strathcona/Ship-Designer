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
            TweakableUpdate,
            "Range");
        resolution = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
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
        p.typeName = "Low Energy Sensor";
        p.modelName = Constants.GetRandomSensorModelName();
        p.range.Value = UnityEngine.Random.Range(2, 20);
        p.resolution.Value = UnityEngine.Random.Range(2, 20);
        return p;

    }

    public override Part Clone() {
        Sensor part = (Sensor)MemberwiseClone();
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
