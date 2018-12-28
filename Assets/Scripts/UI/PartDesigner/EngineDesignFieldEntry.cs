using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class EngineDesignFieldEntry : PartDesignFieldEntry
{
    private Engine engine;

    public Slider agilitySlider;
    public Slider thrustSlider;
    public Text agilityText;
    public Text thrustText;

    public override void Initialize(Part p) {
        engine = new Engine(p);
        UpdateAgilitySlider();
        UpdateThrustSlider();
    }

    public override PartType GetPartType() {
        return PartType.Engine;
    }

    public override Part GetPart() {
        return engine;
    }

    protected override void UpdateStrings() {
        base.UpdateStrings();
        agilityText.text = engine.Agility.ToString();
        thrustText.text = engine.Thrust.ToString();
    }

    public override void SetPart() {
        SetAgilitySlider();
        SetThrustSlider();
        UpdateStrings();
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

    public void SetAgilitySlider() {
        agilitySlider.value = engine.Agility;
    }

    public void UpdateThrustSlider() {
        engine.Thrust = Mathf.FloorToInt(thrustSlider.value);
        UpdateStrings();
    }

    public void SetThrustSlider() {
        thrustSlider.value = engine.Thrust;
    }
}
