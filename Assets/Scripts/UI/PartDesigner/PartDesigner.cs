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
        Debug.Log("Loading Part" + p.modelName +" "+ p.Tier);
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
                activePart = new Engine();
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
    public void ResetCreateNewDropdown() {
        createNewDropdown.value = 0;
    }

    public void AskToCreateNewPart() {
        //check if it's zero because we don't want to trigger a second popup by resetting it to zero
        if(createNewDropdown.value != 0) {
            ModalPopupManager.instance.DisplayModalPopup("Confirmation",
    "Are you sure you want to create a new part?",
    new List<string>() { "Yes", "No" },
    new List<Action>() { CreateNewPart, ResetCreateNewDropdown });
        }
    }

    public void AskToSubmitPart() {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
"Would you like to submit this part for design? It will take "+activePart.ticksToDesign+" units of design effort.",
new List<string>() { "Yes", "No" },
new List<Action>() { SubmitDesign });

        //refresh part list
    }

    public void SubmitDesign() {
        PartLibrary.AddPartToLibrary(activePart);
        partList.DisplayParts();
    }

    public void UpdatePartTier() {
        activePart.Tier = partTierDropdown.value;
        UpdatePartStrings();
    }
}
