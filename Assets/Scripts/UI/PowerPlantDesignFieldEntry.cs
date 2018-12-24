using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantDesignFieldEntry : PartDesignFieldEntry
{
    private PowerPlant powerPlant;

    public Slider outputSlider;
    public Text outputText;

    public override void Initialize() {
        powerPlant = new PowerPlant();
        UpdateOutputSlider();
    }

    public override Part GetPart() {
        return powerPlant;
    }

    protected override void UpdateStrings() {
        base.UpdateStrings();
        outputText.text = powerPlant.NetPower.ToString();
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
