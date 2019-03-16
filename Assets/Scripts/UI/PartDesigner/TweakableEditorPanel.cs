using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class TweakableEditorPanel : MonoBehaviour {
    public Tweakable tweakable;

    public Dropdown dropdown;
    public Text dropdownText;

    public Slider slider;
    public Text sliderDisplay;
    public Text sliderText;
    public Text sliderMinText;
    public Text sliderMaxText;
    public GameObject sliderRuler;

    public void DisplayTweakable(Tweakable t) {
        tweakable = t;
        Clear();
        if(t.tweakableType == TweakableType.Dropdown) {
            SetDropdownElements(true);
            dropdownText.text = tweakable.tweakableName;
            dropdown.ClearOptions();
            foreach(string s in tweakable.dropdownLabels) {
                Dropdown.OptionData option = new Dropdown.OptionData();
                option.text = s;
                dropdown.options.Add(option);
            }
            dropdown.value = tweakable.Value;
            dropdown.RefreshShownValue();
            dropdown.onValueChanged.AddListener(delegate { UpdateFromDropdown(); });

        } else if (t.tweakableType == TweakableType.Slider) {
            SetSliderElements(true);
            sliderText.text = tweakable.tweakableName;

            slider.minValue = tweakable.MinValue;
            slider.maxValue = tweakable.MaxValue;
            slider.value = tweakable.Value;
            sliderMinText.text = slider.minValue.ToString() + tweakable.unit;
            sliderMaxText.text = slider.maxValue.ToString() + tweakable.unit;

            sliderDisplay.text = Mathf.FloorToInt(slider.value).ToString()+tweakable.unit;
            slider.onValueChanged.AddListener(delegate { UpdateFromSlider(); });
        }
    }
    public void UpdateFromDropdown() {
        tweakable.Value = dropdown.value;
    }

    public void UpdateFromSlider() {
        tweakable.Value = Mathf.FloorToInt(slider.value);
        sliderDisplay.text = tweakable.ValueString();
    }


    public void Clear() {
        SetSliderElements(false);
        SetDropdownElements(false);

    }

    public void SetSliderElements(bool on) {
        slider.gameObject.SetActive(on);
        sliderDisplay.gameObject.SetActive(on);
        sliderText.gameObject.SetActive(on);
        sliderRuler.gameObject.SetActive(on);
        sliderMaxText.gameObject.SetActive(on);
        sliderMinText.gameObject.SetActive(on);
    }

    public void SetDropdownElements(bool on) {
        dropdown.gameObject.SetActive(on);
        dropdownText.gameObject.SetActive(on);
    }
}
