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
            TweakableUpdate,
            "Shield Type");
        type.dropdownLabels.Add("Shield Generator");
        type.dropdownLabels.Add("Deflector Shield");

        strength = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Shield Strength");

        rechargeTime = Tweakable.MakeTweakable(
            this,
            TweakableType.Slider,
            TweakableUpdate,
            "Recharge Time");
        rechargeTime.unit = "s";

        tweakables.Add(type);
        tweakables.Add(strength);
        tweakables.Add(rechargeTime);
    }

    protected override void UpdateProperties() {
        base.UpdateProperties();
        weight = Mathf.Max(1, Mathf.FloorToInt(strength.Value / rechargeTime.Value));
    }

    public static Shield GetRandomShield() {
        Shield p = new Shield();
        p.sprite = SpriteLoader.GetPartSprite("defaultShieldS");
        p.Tier = 1;
        p.size = PartSize.S;
        p.shieldType = ShieldType.Generator;
        p.typeName = "Shield Generator";
        p.modelName = Constants.GetRandomSensorModelName();
        p.strength.Value = UnityEngine.Random.Range(2, 20);
        p.rechargeTime.Value = UnityEngine.Random.Range(20, 100);
        return p;

    }

    public override Part Clone() {
        Shield part = (Shield)MemberwiseClone();
        part.manufacturer = manufacturer;
        foreach (Tweakable t in tweakables) {
            Tweakable newt = Tweakable.MakeTweakable(
                 part,
                 t.tweakableType,
                 part.TweakableUpdate,
                 t.tweakableName);
            newt.Value = t.Value;
            newt.dropdownLabels = new List<string>(t.dropdownLabels);
            part.tweakables.Add(newt);
        }
        return part;
    }
}
