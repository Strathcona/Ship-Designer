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
            switch (p.GetPartString()) {
                case "Weapon":
                    Weapon w = (Weapon)p;
                    alphaDamage += w.Damage * w.numberOfPart;
                    averageDamage += w.Damage * w.numberOfPart / w.ReloadTime;
                    break;
                case "FireControl":
                    break;
                case "ShipEngine":
                    Engine e = (Engine)p;
                    speedRating += e.Thrust * e.numberOfPart * 1.0f / hullSize;
                    manevorability += e.Agility * e.numberOfPart / (0.5f * hullSize);
                    break;
                case "Sensor":
                    break;
                case "PowerPlant":
                    break;
                default:
                    Debug.Log("Couldn't find " + p.GetPartString());
                    break;
            }
        }
    }
    public void AddPart(Part p) {
        allParts.Add(p);
        switch (p.GetPartString()) {
            case "Weapon":
                Weapon w = (Weapon)p;
                weapons.Add(w);
                alphaDamage += w.Damage*w.numberOfPart;
                averageDamage += w.Damage*w.numberOfPart / w.ReloadTime;
                break;
            case "FireControl":
                fireControls.Add((FireControl)p);
                break;
            case "ShipEngine":
                Engine e = (Engine)p;
                engines.Add(e);
                speedRating += e.Thrust*e.numberOfPart*1.0f / hullSize;
                manevorability += e.Agility*e.numberOfPart / (0.5f*hullSize);
                break;
            case "Sensor":
                sensors.Add((Sensor)p);
                break;
            case "PowerPlant":
                powerplants.Add((PowerPlant)p);
                break;
            default:
                Debug.Log("Couldn't find "+p.GetPartString());
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
        Engine e = Engine.GetRandomShipEngine();
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
