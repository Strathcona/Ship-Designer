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

    public Sensor(): base() {
        partType = PartType.Sensor;
    }

    public Sensor(Part p) : base() {
        Sensor s = (Sensor)p;
        range.Value = s.range.Value;
        resolution.Value = s.resolution.Value;
        partType = PartType.Sensor;
        UpdateProperties();
    }

    protected override void InitializeTweakables() {
        range = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Range");
        resolution = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Resolution");

        tweakables.Add(range);
        tweakables.Add(resolution);
    }

    public override void CopyValuesFromPart(Part p) {
        base.CopyValuesFromPart(p);
        Sensor s = (Sensor)p;
        range.Value = s.range.Value;
        resolution.Value= s.resolution.Value;
        partType = PartType.Sensor;
        UpdateProperties();
    }

    public static Sensor GetRandomSensor() {
        Sensor p = new Sensor();
        p.sprite = SpriteLoader.GetPartSprite("defaultSensorS");
        p.Tier = 1;
        p.size = PartSize.S;
        p.sensorType = SensorType.LowEnergy;
        p.typeName = "Low Energy Sensor";
        p.modelName = Constants.GetRandomSensorModelName();
        p.range.Value = UnityEngine.Random.Range(2, 20);
        p.resolution.Value = UnityEngine.Random.Range(2, 20);
        return p;

    }
}
