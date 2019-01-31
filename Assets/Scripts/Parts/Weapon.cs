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

    protected override void InitializeTweakables() {
        caliber = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            5,
            5,
            5,
            100,
            "Caliber");
        caliber.unit = "mm";

        reload = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            5,
            5,
            5,
            100,
            "Reload");
        reload.unit = "s";
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

    public override string GetDescriptionString() {
        string caliberString = caliber.ValueString();
        string partTypeName = Constants.GetWeaponTypeName(tier, weaponType.Value);
        string typeline;
        if(manufacturer != null) {
            typeline = manufacturer.name + " " + modelName + " " + partTypeName;
        } else {
            typeline = modelName + " " + partTypeName;
        }
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
        return turretSetup+" "+ caliberString + " " + typeline;
    }

    public override string GetStatisticsString() {
        return "Weight: " + weight.ToString() + " Damage: " + damage + " Recharge Time: " + reload.ValueString();
    }

    protected override void UpdateProperties() {
        base.UpdateProperties();
        int turretfactor = Mathf.Max(1, turrets.Value);
        weight = Mathf.Max(1, Mathf.FloorToInt(caliber.Value / reload.Value)*turretfactor);
        damage = Mathf.FloorToInt(caliber.Value * Constants.TierDamagePerSize[tier]) * turretfactor ;
    }

    public static Weapon GetRandomLaser() {
        Weapon p = new Weapon();
        p.sprite = SpriteLoader.GetPartSprite("defaultWeaponS");
        p.Tier = (Random.Range(1, 6));
        p.weaponType.Value = 0;
        p.size = PartSize.S;
        p.caliber.Value = Random.Range(2, 20);
        p.turrets.Value = Random.Range(0, 5);
        p.reload.Value = Random.Range(2, 20);
        p.modelName = Constants.GetRandomWeaponModelName();
        Debug.Log(p.GetDescriptionString());
        Debug.Log(p.GetStatisticsString());
        return p;
    }

    public override Part Clone() {
        Weapon part = (Weapon) MemberwiseClone();
        part.manufacturer = manufacturer;
        foreach (Tweakable t in tweakables) {
            Tweakable newt = Tweakable.MakeTweakable(
                part,
                t.tweakableType,
                part.TweakableUpdate,
                t.Value,
                t.DefaultValue,
                t.MinValue,
                t.MaxValue,
                t.tweakableName);
            newt.dropdownLabels = new List<string>(t.dropdownLabels);
            part.tweakables.Add(newt);
        }
        return part;
    }
}
