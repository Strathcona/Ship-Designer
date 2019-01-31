using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class TweakableEditorPanel : MonoBehaviour {
    public Action partDesignerUpdateStrings; 

    public Tweakable tweakable;

    public Dropdown dropdown;
    public Text dropdownText;

    public Slider slider;
    public Text sliderDisplay;
    public Text sliderText;

    public void DisplayTweakable(Tweakable t) {
        tweakable = t;
        Clear();
        if(t.tweakableType == TweakableType.Dropdown) {
            dropdown.gameObject.SetActive(true);
            dropdownText.gameObject.SetActive(true);
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
            slider.gameObject.SetActive(true);
            sliderDisplay.gameObject.SetActive(true);
            sliderText.gameObject.SetActive(true);
            sliderText.text = tweakable.tweakableName;

            slider.minValue = tweakable.MinValue;
            slider.maxValue = tweakable.MaxValue;
            slider.value = tweakable.Value;

            sliderDisplay.text = Mathf.FloorToInt(slider.value).ToString()+tweakable.unit;
            slider.onValueChanged.AddListener(delegate { UpdateFromSlider(); });
            partDesignerUpdateStrings();
        }
    }
    public void UpdateFromDropdown() {
        tweakable.Value = dropdown.value;
        partDesignerUpdateStrings();
    }

    public void UpdateFromSlider() {
        tweakable.Value = Mathf.FloorToInt(slider.value);
        sliderDisplay.text = tweakable.ValueString();
        partDesignerUpdateStrings();
    }


    public void Clear() {
        dropdown.gameObject.SetActive(false);
        dropdownText.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        sliderDisplay.gameObject.SetActive(false);
        sliderText.gameObject.SetActive(false);
    }
}
