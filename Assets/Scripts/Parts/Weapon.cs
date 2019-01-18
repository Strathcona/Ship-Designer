using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class Weapon : Part {
    public Tweakable caliber;
    public Tweakable reload;
    public Tweakable turrets;
    public Tweakable weaponType;

    private int damage;
    public int Damage {
        get { return damage; }
    }

    public Weapon() : base(){
        partType = PartType.Weapon;
    }

    public Weapon(Part p) : base() {
        Weapon w = (Weapon)p;
        for (int i = 0; i < w.tweakables.Count; i++) {
            tweakables[i].Value = w.tweakables[i].Value;
        }
        partType = PartType.Weapon;
    }

    protected override void InitializeTweakables() {
        caliber = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Caliber");

        reload = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            1,
            1,
            1,
            100,
            "Reload");
        turrets = Tweakable.MakeTweakable(
            this,
            TweakableType.Dropdown,
            TweakableUpdate,
            0,
            0,
            0,
            4,
            "Turret Setup");
        turrets.dropdownLabels.Add("Centerline Mounted");
        turrets.dropdownLabels.Add("Single Turret");
        turrets.dropdownLabels.Add("Dual Turret");
        turrets.dropdownLabels.Add("Triple Turret");
        turrets.dropdownLabels.Add("Quadruple Turret");

        weaponType = Tweakable.MakeTweakable(
            this,
            TweakableType.Dropdown,
            TweakableUpdate,
            0,
            0,
            0,
            1,
            "Weapon Type");
        weaponType.dropdownLabels.Add("Laser");
        weaponType.dropdownLabels.Add("Railgun");
        tweakables.Add(weaponType);
        tweakables.Add(turrets);
        tweakables.Add(caliber);
        tweakables.Add(reload);
    }

    public override void CopyValuesFromPart(Part p) {
        base.CopyValuesFromPart(p);
        Weapon w = (Weapon)p;
        for (int i = 0; i < w.tweakables.Count; i++) {
            tweakables[i].Value = w.tweakables[i].Value;
        }
        partType = PartType.Weapon;
    }

    public override string GetDescriptionString() {
        string caliberString = caliber.Value.ToString() + "mm";
        string partTypeName = Constants.GetWeaponTypeName(tier, weaponType.Value);
        string typeline = manufacturerName + " " + modelName + " " + partTypeName;
;
        string turretSetup = "";
        switch (turrets.Value) {
            case 0:
                turretSetup = "Centerline Mounted";
                break;
            case 1:
                turretSetup = "Single Turret";
                break;
            case 2:
                turretSetup = "Double Turret";
                break;
            case 3:
                turretSetup = "Triple Turret";
                break;
            case 4:
                turretSetup = "Quadruple Turret";
                break;
            default:
                break;
        }
        return turretSetup+" "+ caliberString + typeline;
    }

    public override string GetStatisticsString() {
        return "Size: " + size.ToString() + " Damage: " + damage + " Recharge Time: " + reload.Value.ToString() + "s";
    }

    protected override void UpdateProperties() {
        base.UpdateProperties();
        int turretfactor = Mathf.Max(1, turrets.Value);
        size = Mathf.Max(1, Mathf.FloorToInt(caliber.Value / reload.Value) + turretfactor);
        damage = Mathf.FloorToInt(caliber.Value * Constants.TierDamagePerSize[tier]) * turretfactor ;
    }

    public override void TweakableUpdate() {
        UpdateProperties();
    }

    public static Weapon GetRandomLaser() {
        Weapon w = new Weapon();
        w.Tier = (Random.Range(1, 6));
        w.weaponType.Value = 0;
        w.caliber.Value = Random.Range(2, 20);
        w.turrets.Value = Random.Range(0, 5);
        w.reload.Value = Random.Range(2, 20);
        w.modelName = Constants.GetRandomWeaponModelName();
        Debug.Log(w.GetDescriptionString());
        Debug.Log(w.GetStatisticsString());
        return w;
    }
}
