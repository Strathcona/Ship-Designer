using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorDesignFieldEntry : PartDesignFieldEntry
{
    private Sensor sensor;

    public Slider rangeSlider;
    public Slider resolutionSlider;
    public Text rangeText;
    public Text resolutionText;

    public override void Initialize() {
        sensor = new Sensor();
        UpdateRangeSlider();
        UpdateResolutionSlider();
    }

    public override Part GetPart() {
        return sensor;
    }

    protected override void UpdateStrings() {
        base.UpdateStrings();
        rangeText.text = sensor.Range.ToString();
        resolutionText.text = sensor.Resolution.ToString();
    }

    public override void Clear() {
        rangeSlider.value = rangeSlider.minValue;
        resolutionSlider.value = resolutionSlider.minValue;
        UpdateStrings();
    }
    
    public void UpdateRangeSlider() {
        sensor.Range = Mathf.FloorToInt(rangeSlider.value);
        UpdateStrings();
    }

    public void UpdateResolutionSlider() {
        sensor.Resolution = Mathf.FloorToInt(resolutionSlider.value);
        UpdateStrings();

    }


}
