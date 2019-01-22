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


    public override string GetDescriptionString() {
        return manufacturerName + " " + modelName + " " + typeName;
    }
    public override string GetStatisticsString() {
        return "Size: " + size.ToString() + " Range: " + range.Value.ToString() + " Resoultion " + resolution.Value.ToString();
    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(range.Value * resolution.Value * 0.1f));
    }

    public override void TweakableUpdate() {

    }

    public static Sensor GetRandomSensor() {
        Sensor s = new Sensor();
        s.Tier = 1;
        s.sensorType = SensorType.LowEnergy;
        s.typeName = "Low Energy Sensor";
        s.modelName = Constants.GetRandomSensorModelName();
        s.range.Value = UnityEngine.Random.Range(2, 20);
        s.resolution.Value = UnityEngine.Random.Range(2, 20);
        return s;

    }
}
