using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class FireControlDesignFieldEntry : PartDesignFieldEntry
{
    private FireControl fireControl;

    public Slider trackingSlider;
    public Slider accuracySlider;
    public Slider rangeSlider;
    public Text trackingText;
    public Text accuracyText;
    public Text rangeText;

    public override void Initialize(Part p) {
        fireControl = new FireControl(p);
        UpdateAccuracySlider();
        UpdateRangeSlider();
        UpdateTrackingSlider();
    }

    public override PartType GetPartType() {
        return PartType.FireControl;
    }

    public override Part GetPart() {
        return fireControl;
    }

    protected override void UpdateStrings() {
        base.UpdateStrings();
        trackingText.text = fireControl.Tracking.ToString();
        accuracyText.text = fireControl.Accuracy.ToString();
        rangeText.text = fireControl.Range.ToString();
    }

    public override void SetPart() {
        SetAccuracySlider();
        SetTrackingSlider();
        SetRangeSlider();
        UpdateStrings();
    }

    public override void Clear() {
        trackingSlider.value = trackingSlider.minValue;
        accuracySlider.value = accuracySlider.minValue;
        rangeSlider.value = rangeSlider.minValue;
        UpdateStrings();
    }

    public void UpdateTrackingSlider() {
        fireControl.Tracking = Mathf.FloorToInt(trackingSlider.value);
        UpdateStrings();
    }

    public void SetTrackingSlider() {
        trackingSlider.value = fireControl.Tracking;
    }

    public void UpdateAccuracySlider() {
        fireControl.Accuracy = Mathf.FloorToInt(accuracySlider.value);
        UpdateStrings();
    }

    public void SetAccuracySlider() {
        accuracySlider.value = fireControl.Accuracy;
    }

    public void UpdateRangeSlider() {
        fireControl.Range = Mathf.FloorToInt(rangeSlider.value);
        UpdateStrings();
    }

    public void SetRangeSlider() {
        rangeSlider.value = fireControl.Range;
    }
}
