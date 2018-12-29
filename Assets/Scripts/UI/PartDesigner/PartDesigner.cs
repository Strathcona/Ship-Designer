using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PartDesigner : MonoBehaviour {
    public Part activePart;
    public Dropdown partTierDropdown;
    public Dropdown createNewDropdown;
    public Text descriptionDisplay;
    public Text statisticsDisplay;
    public InputField modelNameInput;
    public PartList partList;
    public TweakableEditor tweakableEditor;

    private void Awake() {
        partList.partDesigner = this;
        partList.DisplayParts();
    }

    public void UpdatePartModel() {
        activePart.modelName = modelNameInput.text;
        UpdatePartStrings();
    }

    public void SetPartModel() {
        modelNameInput.text = activePart.modelName;
    }

    public void UpdatePartStrings() {
        if(activePart != null) {
            descriptionDisplay.text = activePart.GetDescriptionString();
            statisticsDisplay.text = activePart.GetStatisticsString();
        }
    }

    public void LoadPart(Part p) {
        switch (p.partType) {
            case PartType.Weapon:
                activePart = new Weapon();
                break;
            case PartType.Sensor:
                activePart = new Sensor();
                break;
            case PartType.PowerPlant:
                activePart = new PowerPlant();
                break;
            case PartType.FireControl:
                activePart = new FireControl();
                break;
            case PartType.Engine:
                activePart = new FireControl();
                break;
        }
        activePart.CopyValuesFromPart(p);
        modelNameInput.text = activePart.modelName;
        SetPartModel();
        partTierDropdown.value = activePart.Tier;
        tweakableEditor.DisplayTweakables(activePart);
    }

    public void Clear() {
        tweakableEditor.Clear();
        descriptionDisplay.text = "---";
        statisticsDisplay.text = "---";

    }

    public void CreateNewPart() {
        switch (createNewDropdown.value) {
            case 0:
                Debug.Log("This is the default value, nothing should change");
                break;
            case 1:
                activePart = new Weapon();
                break;
            case 2:
                activePart = new FireControl();
                break;
            case 3:
                activePart = new Engine();
                break;
            case 4:
                activePart = new PowerPlant();
                break;
            case 5:
                activePart = new Sensor();
                break;
        }
        createNewDropdown.value = 0;
        tweakableEditor.DisplayTweakables(activePart);
    }

    public void SubmitDesign() {

    }

    public void UpdatePartTier() {
        activePart.Tier = partTierDropdown.value;
        UpdatePartStrings();
    }
}
