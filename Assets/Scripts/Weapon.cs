using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Weapon : Part {
    private int caliber;
    public int Caliber {
        get { return caliber; }
        set {
            caliber = value;
            UpdateProperties();
        }
    }
    private float reloadTime;
    public float ReloadTime {
        get { return reloadTime; }
        set {
            reloadTime = value;
            UpdateProperties();
        }
    }
    private int damage;
    public int Damage {
        get { return damage; }
    }
    private bool turreted;
    public bool Turreted {
        get { return turreted; }
    }
    private int turretNumber;
    public int TurretNumber {
        get { return turretNumber; }
    }
    public WeaponType weaponType;


    public Weapon() {
        partType = PartType.Weapon;
    }

    public override string GetDescriptionString() {
        string number = (numberOfPart + " x");
        string caliberString = caliber.ToString() + "mm";
        string partTypeName = Constants.GetWeaponTypeName(tier, weaponType);
        string typeline = manufacturerName + " " + partModelName + " " + partTypeName;
;
        string turretSetup = "";
        switch (turretNumber) {
            case 1:
                if(turreted == false) {
                    turretSetup = "Centerline Mounted";
                } else {
                    turretSetup = "Single Turret";
                }
                break;
            case 2:
                turretSetup = "Double Turret";
                break;
            case 3:
                turretSetup = "Triple Turret";
                break;
            case 4:
                turretSetup = "Quad Turret";
                break;
            default:
                break;
        }
        return turretSetup+" "+ caliberString + " " + typeline;
    }

    public override string GetStatisticsString() {
        return "Size: "+ this.GetTotalSize().ToString()+ " Damage: " + damage + " Recharge Time: " + reloadTime + "s";
    }

    public void SetTurrets(bool _turreted, int _turretNumber) {
        turreted = _turreted;
        turretNumber = _turretNumber;
        UpdateProperties();
    }

    public override string GetPartString() {
        return "Weapon";
    }
    
    protected override void UpdateProperties() {
        int turretfactor = 0;
        if (turreted) {
            turretfactor = 1;
        }
        size = Mathf.Max(1, Mathf.FloorToInt(caliber / reloadTime) + turretfactor);
        damage = Mathf.FloorToInt(caliber * Constants.TierDamagePerSize[tier]) * turretNumber;
        creditCost = Mathf.FloorToInt(size * 120.0f + reloadTime*60f);
        timeCost = Mathf.FloorToInt(reloadTime * 10.0f + size * 5.0f);
    }

    public static Weapon GetRandomLaser() {
        Weapon w = new Weapon();
        w.Tier = (Random.Range(1, 6));
        w.weaponType = WeaponType.laser;
        w.Caliber = Random.Range(2, 20)*4;
        w.manufacturerName = Constants.GetRandomCompanyName();
        if(Random.Range(0,2) == 1) {
            w.SetTurrets(true, Random.Range(1, 5));
        } else {
            w.SetTurrets(false, 1);
        }
        w.ReloadTime = (w.Caliber * Constants.TierFireTimePerSize[w.Tier]);
        w.partModelName = Constants.GetRandomWeaponModelName();
        w.numberOfPart = Random.Range(1, 4);
        Debug.Log(w.GetDescriptionString());
        Debug.Log(w.GetStatisticsString());
        return w;
    }
}
