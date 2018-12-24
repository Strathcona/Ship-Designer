using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameConstructs;

public class Sensor : Part{
    private int range;
    public int Range {
        get { return range; }
        set {
            range = value;
            UpdateProperties();
        }
    }
    private int resolution;
    public int Resolution {
        get { return resolution; }
        set {
            resolution = value;
            UpdateProperties();
        }
    }
    public SensorType sensorType;

    public Sensor() {
        partType = PartType.Sensor;
    }

    public override string GetDescriptionString() {
        return manufacturerName + " " + partModelName + " " + partTypeName;
    }
    public override string GetStatisticsString() {
        return "Size: " + size.ToString() + " Range: " + Range.ToString() + " Resoultion " + Resolution.ToString();
    }
    public override string GetPartString() {
        return "Sensor";
    }

    protected override void UpdateProperties() {
        size = Mathf.Max(1, Mathf.FloorToInt(range * resolution * 0.1f));
    }

    public static Sensor GetRandomSensor() {
        Sensor s = new Sensor();
        s.Tier = 1;
        s.sensorType = SensorType.LowEnergy;
        s.partTypeName = "Low Energy Sensor";
        s.partModelName = Constants.GetRandomSensorModelName();
        s.manufacturerName = Constants.GetRandomCompanyName();
        s.Range = UnityEngine.Random.Range(2, 20);
        s.Resolution = UnityEngine.Random.Range(2, 20);
        return s;

    }
}
