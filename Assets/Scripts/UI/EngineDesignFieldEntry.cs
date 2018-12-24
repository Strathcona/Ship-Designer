using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineDesignFieldEntry : PartDesignFieldEntry
{
    private Engine engine;

    public Slider agilitySlider;
    public Slider thrustSlider;
    public Text agilityText;
    public Text thrustText;

    public override void Initialize() {
        engine = new Engine();
        UpdateAgilitySlider();
        UpdateThrustSlider();
    }

    public override Part GetPart() {
        return engine;
    }

    protected override void UpdateStrings() {
        base.UpdateStrings();
        agilityText.text = engine.Agility.ToString();
        thrustText.text = engine.Thrust.ToString();
    }

    public override void Clear() {
        agilitySlider.value = agilitySlider.minValue;
        thrustSlider.value = thrustSlider.minValue;
        UpdateStrings();
    }

    public void UpdateAgilitySlider() {
        engine.Agility = Mathf.FloorToInt(agilitySlider.value);
        UpdateStrings();
    }

    public void UpdateThrustSlider() {
        engine.Thrust = Mathf.FloorToInt(thrustSlider.value);
        UpdateStrings();
    }
}
