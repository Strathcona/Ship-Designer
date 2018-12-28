using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PowerPlantDesignFieldEntry : PartDesignFieldEntry
{
    private PowerPlant powerPlant;

    public Slider outputSlider;
    public Text outputText;

    public override void Initialize(Part p) {
        powerPlant = new PowerPlant(p);
        UpdateOutputSlider();
    }

    public override PartType GetPartType() {
        return PartType.PowerPlant;
    }

    public override Part GetPart() {
        return powerPlant;
    }

    protected override void UpdateStrings() {
        base.UpdateStrings();
        outputText.text = powerPlant.NetPower.ToString();
    }

    public override void SetPart() {
        UpdateStrings();
    }

    public override void Clear() {
        outputSlider.value = outputSlider.minValue;
        UpdateStrings();
    }

    public void UpdateOutputSlider() {
        powerPlant.NetPower = Mathf.FloorToInt(outputSlider.value);
        UpdateStrings();
    }
}
