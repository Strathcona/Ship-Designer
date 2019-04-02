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

    public int Damage { get { return caliber.Value * turretfactor; } }
    public int MaxDamage { get { return caliber.MaxValue * turretfactor; } }
    public int DamagePerSecond { get { return caliber.Value / reload.Value * turretfactor; } }
    public int MaxDamagePerSecond { get { return caliber.MaxValue / reload.MinValue * turretfactor; } }
    private int turretfactor = 1;
    public Weapon() {
        partType = PartType.Weapon;
        InitializeTweakables();
    }

    protected override void InitializeTweakables() {
        caliber = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Caliber");
        caliber.unit = "mm";

        reload = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Reload Time");
        reload.unit = "s";

        turrets = Tweakable.MakeTweakable(
            this,
            TweakableType.Dropdown,
            "Turret Setup");
        
        weaponType = Tweakable.MakeTweakable(
            this,
            TweakableType.Dropdown,
            "Weapon Type");
        
        tweakables.Add(weaponType);
        tweakables.Add(turrets);
        tweakables.Add(caliber);
        tweakables.Add(reload);

        caliber.MaxCost = 25;
        caliber.MaxWeight = 30;
        reload.ReverseScaling = true; //because reloading faster is worth more;
        reload.MaxNetPower = 25;
        turrets.automaticCalculation = false;
        turrets.dropdownLabels.Add("Centerline Mounted");
        turrets.dropdownLabels.Add("Single Turret");
        turrets.dropdownLabels.Add("Dual Turret");
        turrets.dropdownLabels.Add("Triple Turret");
        turrets.dropdownLabels.Add("Quadruple Turret");
        weaponType.dropdownLabels.Add("Laser");
        weaponType.dropdownLabels.Add("Railgun");
        weaponType.automaticCalculation = false;
    }

    public override string GetDescriptionString() {
        string caliberString = caliber.ValueString();
        string partTypeName = Constants.GetPartDescriptionName(this);
        string typeline;
        if(Manufacturer != null) {
            typeline = Manufacturer.name + " " + ModelName + " " + partTypeName;
        } else {
            typeline = ModelName + " " + partTypeName;
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
        return "Weight: " + Weight.ToString() + " Damage: " + Damage + " Recharge Time: " + reload.ValueString();
    }

    protected override void UpdateProperties() {
        base.UpdateProperties();
        turretfactor = Mathf.Max(1, turrets.Value);
        cost = cost * turretfactor;
        maxCost = maxCost * turretfactor;
        designCost = designCost * turretfactor;
        maxDesignCost = maxDesignCost * turretfactor;
        Weight = Weight * turretfactor;
        maxWeight = maxWeight * turretfactor;
    }

    public override Dictionary<string, float> GetNormalizedPerformanceValues() {
        Dictionary<string, float> dict = base.GetNormalizedPerformanceValues();
        dict.Add("Damage", (float) Damage/MaxDamage);
        dict.Add("DPS", (float) DamagePerSecond/MaxDamagePerSecond);
        
       return dict;
    }

    public static Weapon GetRandomLaser() {
    Weapon p = new Weapon();
    p.sprite = SpriteLoader.GetPartSprite("defaultWeaponS");
    p.Tier = (Random.Range(1, 6));
    p.weaponType.Value = 0;
    p.Size = PartSize.S;
    p.caliber.Value = Random.Range(2, 20);
    p.turrets.Value = Random.Range(0, 5);
    p.reload.Value = Random.Range(2, 20);
    p.ModelName = StringLoader.GetAString("weaponNames");
    Debug.Log(p.GetDescriptionString());
    Debug.Log(p.GetStatisticsString());
    return p;
    }

    public override Part Clone() {
        Weapon part = (Weapon) MemberwiseClone();
        List<Tweakable> newTweakables = new List<Tweakable>();
        part.Manufacturer = Manufacturer;
        foreach (Tweakable t in tweakables) {
            Tweakable newt = Tweakable.MakeTweakable(
                part,
                t.tweakableType,
                t.tweakableName);
            newt.Value = t.Value;
            newt.dropdownLabels = new List<string>(t.dropdownLabels);
            newTweakables.Add(newt);
        }
        part.tweakables = newTweakables;
        part.UpdateProperties();
        return part;
    }
}
