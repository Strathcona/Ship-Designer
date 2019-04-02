using UnityEngine;
using System.Collections.Generic;
using GameConstructs;

public class Shield : Part {
    public Tweakable type;
    public Tweakable strength;
    public Tweakable rechargeTime;
    public ShieldType shieldType;

    public Shield() {
        partType = PartType.Shield;
        InitializeTweakables();
    }

    protected override void InitializeTweakables() {
        type = Tweakable.MakeTweakable(
            this,
            TweakableType.Dropdown,
            "Shield Type");

        strength = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Shield Strength");

        rechargeTime = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            "Recharge Time");
        rechargeTime.unit = "s";

        tweakables.Add(type);
        tweakables.Add(strength);
        tweakables.Add(rechargeTime);

        strength.MaxNetPower = 35;
        rechargeTime.MaxNetPower = 25;
        type.automaticCalculation = false;
        type.dropdownLabels.Add("Shield Generator");
        type.dropdownLabels.Add("Deflector Shield");
    }

    protected override void UpdateProperties() {
        base.UpdateProperties();
        Weight = Mathf.Max(1, Mathf.FloorToInt(strength.Value / rechargeTime.Value));
    }

    public static Shield GetRandomShield() {
        Shield p = new Shield();
        p.sprite = SpriteLoader.GetPartSprite("defaultShieldS");
        p.Tier = 1;
        p.Size = PartSize.S;
        p.shieldType = ShieldType.Generator;
        p.DescriptionName = "Shield Generator";
        p.ModelName = StringLoader.GetAString("sensorNames");
        p.strength.Value = UnityEngine.Random.Range(2, 20);
        p.rechargeTime.Value = UnityEngine.Random.Range(20, 100);
        return p;

    }

    public override Part Clone() {
        Shield part = (Shield)MemberwiseClone();
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
