using System.Collections;
using GameConstructs;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Ship {
    public string shipName;
    public string className;
    public List<HullSpace> hullSpaces;
    public int lifeSupportSize;
    public float speedRating;
    public float manevorability;
    public float alphaDamage;
    public float averageDamage;
    public int minutesToDevelop = 30000;
    public Dictionary<Part, int> parts = new Dictionary<Part, int>();
    public List<Weapon> weapons = new List<Weapon>();
    public List<PowerPlant> powerplants = new List<PowerPlant>();
    public List<Sensor> sensors = new List<Sensor>();
    public List<FireControl> fireControls = new List<FireControl>();
    public List<Engine> engines = new List<Engine>();
    // Start is called before the first frame update


    public void RecalculatePartStats() {
        foreach(Part p in parts.Keys) {
            switch (p.partType) {
                case PartType.Weapon:
                    Weapon w = (Weapon)p;
                    alphaDamage += w.Damage * parts[p];
                    averageDamage += w.Damage * parts[p] / w.reload.Value;
                    break;
                case PartType.FireControl:
                    break;
                case PartType.Engine:
                    Engine e = (Engine)p;
                    speedRating += e.thrust.Value * parts[p] * 1.0f / hull;
                    manevorability += e.agility.Value * parts[p] / (0.5f * hull);
                    break;
                case PartType.Sensor:
                    break;
                case PartType.PowerPlant:
                    break;
                default:
                    Debug.LogError("Couldn't find " + p.partType);
                    break;
            }
        }
    }
    public bool AddPart(Part p, int number) {
        parts.Add(p, number);
        switch (p.partType) {
            case PartType.Weapon:
                Weapon w = (Weapon)p;
                weapons.Add(w);
                alphaDamage += w.Damage* parts[p];
                averageDamage += w.Damage* parts[p] / w.reload.Value;
                break;
            case PartType.FireControl:
                fireControls.Add((FireControl)p);
                break;
            case PartType.Engine:
                Engine e = (Engine)p;
                engines.Add(e);
                speedRating += e.thrust.Value* parts[p] * 1.0f / hull;
                manevorability += e.agility.Value* parts[p] / (0.5f*hull);
                break;
            case PartType.Sensor:
                sensors.Add((Sensor)p);
                break;
            case PartType.PowerPlant:
                powerplants.Add((PowerPlant)p);
                break;
            default:
                Debug.Log("Couldn't find "+p.partType);
                break;
        }
    }

    public static Ship MakeUpARandomShip(){
        Ship s = new Ship();
        s.shipName = Constants.GetRandomShipName();
        s.className = Constants.GetRandomShipName();
        s.hull = 250;
        int rand = UnityEngine.Random.Range(1, 4);
        for(int  i = 0; i <= rand; i++) {
            s.AddPart(Weapon.GetRandomLaser(), UnityEngine.Random.Range(1,3));
        }
        FireControl f = FireControl.GetRandomFireControl();
        s.AddPart(f, UnityEngine.Random.Range(1, 3));
        Engine e = Engine.GetRandomEngine();
        s.AddPart(e, UnityEngine.Random.Range(1, 3));
        Sensor sen = Sensor.GetRandomSensor();
        s.AddPart(sen, UnityEngine.Random.Range(1, 3));
        PowerPlant pp = PowerPlant.GetRandomPowerPlant();
        s.AddPart(pp, UnityEngine.Random.Range(1, 3));
        s.hull = 0;
        foreach(Part p in s.parts.Keys) {
            s.hull += p.Size * s.parts[p];
        }
        s.hull += UnityEngine.Random.Range(0, 10);
        s.RecalculatePartStats();
        return s;
    }
}
