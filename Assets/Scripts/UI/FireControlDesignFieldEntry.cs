using UnityEngine;
using UnityEngine.UI;

public class FireControlDesignFieldEntry : PartDesignFieldEntry
{
    private FireControl fireControl;

    public Slider trackingSlider;
    public Slider accuracySlider;
    public Slider rangeSlider;
    public Text trackingText;
    public Text accuracyText;
    public Text rangeText;

    public override void Initialize() {
        fireControl = new FireControl();
        UpdateAccuracySlider();
        UpdateRangeSlider();
        UpdateTrackingSlider();
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

    public void UpdateAccuracySlider() {
        fireControl.Accuracy = Mathf.FloorToInt(accuracySlider.value);
        UpdateStrings();
    }

    public void UpdateRangeSlider() {
        fireControl.Range = Mathf.FloorToInt(rangeSlider.value);
        UpdateStrings();
    }
}
