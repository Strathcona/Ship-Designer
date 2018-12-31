using System.Collections;
using GameConstructs;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Ship {
    public string shipName;
    public string className;
    public int hullSize;
    public float speedRating;
    public float manevorability;
    public float alphaDamage;
    public float averageDamage;
    public List<Part> allParts = new List<Part>();
    public List<Weapon> weapons = new List<Weapon>();
    public List<PowerPlant> powerplants = new List<PowerPlant>();
    public List<Sensor> sensors = new List<Sensor>();
    public List<FireControl> fireControls = new List<FireControl>();
    public List<Engine> engines = new List<Engine>();
    // Start is called before the first frame update

    public void RecalculatePartStats() {
        foreach(Part p in allParts) {
            switch (p.partType) {
                case PartType.Weapon:
                    Weapon w = (Weapon)p;
                    alphaDamage += w.Damage * w.numberOfPart;
                    averageDamage += w.Damage * w.numberOfPart / w.reload.Value;
                    break;
                case PartType.FireControl:
                    break;
                case PartType.Engine:
                    Engine e = (Engine)p;
                    speedRating += e.thrust.Value * e.numberOfPart * 1.0f / hullSize;
                    manevorability += e.agility.Value * e.numberOfPart / (0.5f * hullSize);
                    break;
                case PartType.Sensor:
                    break;
                case PartType.PowerPlant:
                    break;
                default:
                    Debug.Log("Couldn't find " + p.partType);
                    break;
            }
        }
    }
    public void AddPart(Part p) {
        allParts.Add(p);
        switch (p.partType) {
            case PartType.Weapon:
                Weapon w = (Weapon)p;
                weapons.Add(w);
                alphaDamage += w.Damage*w.numberOfPart;
                averageDamage += w.Damage*w.numberOfPart / w.reload.Value;
                break;
            case PartType.FireControl:
                fireControls.Add((FireControl)p);
                break;
            case PartType.Engine:
                Engine e = (Engine)p;
                engines.Add(e);
                speedRating += e.thrust.Value*e.numberOfPart*1.0f / hullSize;
                manevorability += e.agility.Value*e.numberOfPart / (0.5f*hullSize);
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
        s.hullSize = 250;
        int rand = UnityEngine.Random.Range(1, 4);
        for(int  i = 0; i <= rand; i++) {
            s.AddPart(Weapon.GetRandomLaser());
        }
        FireControl f = FireControl.GetRandomFireControl();
        s.AddPart(f);
        Engine e = Engine.GetRandomEngine();
        s.AddPart(e);
        Sensor sen = Sensor.GetRandomSensor();
        s.AddPart(sen);
        PowerPlant pp = PowerPlant.GetRandomPowerPlant();
        s.AddPart(pp);
        s.hullSize = 0;
        foreach(Part p in s.allParts) {
            s.hullSize += p.Size;
        }
        s.hullSize += UnityEngine.Random.Range(0, 10);
        s.RecalculatePartStats();
        return s;
    }
}
