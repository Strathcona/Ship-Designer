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
    public Dropdown partSizeDropdown;
    public Text descriptionDisplay;
    public Text statisticsDisplay;
    public InputField modelNameInput;
    public TweakableEditor tweakableEditor;
    public Image previewImage;

    public void Start() {
        modelNameInput.gameObject.SetActive(false);
        partTierDropdown.gameObject.SetActive(false);
        partSizeDropdown.gameObject.SetActive(false);
        previewImage.gameObject.SetActive(false);
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

    public void AskToLoadPart() {
        PartLoader.instance.LoadPartPopup(LoadPart);
    }

    public void LoadPart(Part p) {
        modelNameInput.gameObject.SetActive(true);
        partTierDropdown.gameObject.SetActive(true);
        partSizeDropdown.gameObject.SetActive(true);
        previewImage.gameObject.SetActive(true);
        Debug.Log("Loading Part" + p.modelName +" "+ p.Tier);
        switch (p.partType) {
            case PartType.Weapon:
                activePart = new Weapon();
                break;
            case PartType.Sensor:
                activePart = new Sensor();
                break;
            case PartType.Reactor:
                activePart = new Reactor();
                break;
            case PartType.FireControl:
                activePart = new FireControl();
                break;
            case PartType.Engine:
                activePart = new Engine();
                break;
        }
        activePart.CopyValuesFromPart(p);
        previewImage.sprite = activePart.sprite;
        modelNameInput.text = activePart.modelName;
        SetPartModel();
        partTierDropdown.value = activePart.Tier;
        tweakableEditor.DisplayTweakables(activePart);
    }

    public void Clear() {
        tweakableEditor.Clear();
        descriptionDisplay.text = "---";
        statisticsDisplay.text = "---";
        modelNameInput.gameObject.SetActive(false);
        partTierDropdown.gameObject.SetActive(false);
        previewImage.gameObject.SetActive(false);
        partSizeDropdown.gameObject.SetActive(false);
    }

    public void CreateNewPart() {
        modelNameInput.gameObject.SetActive(true);
        partTierDropdown.gameObject.SetActive(true);
        previewImage.gameObject.SetActive(true);
        partSizeDropdown.gameObject.SetActive(true);

        switch (createNewDropdown.value) {
            case 0:
                break;
            case 1:
                activePart = new Weapon();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultWeaponS");
                break;
            case 2:
                activePart = new FireControl();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultFireControlS");
                break;
            case 3:
                activePart = new Engine();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultEngineS");
                break;
            case 4:
                activePart = new Reactor();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultReactorS");
                break;
            case 5:
                activePart = new Sensor();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultSensorS");
                break;
        }
        activePart.sprite = previewImage.sprite;
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
"Would you like to submit this part for design? It will take "+TimeManager.GetTimeString(activePart.minutesToDevelop)+".",
new List<string>() { "Yes", "No" },
new List<Action>() { SubmitDesign });

        //refresh part list
    }

    public void SubmitDesign() {
        PartLibrary.AddPartToDevelopment(activePart);
    }

    public void UpdatePartTier() {
        activePart.Tier = partTierDropdown.value;
        UpdatePartStrings();
    }

    public void UpdatePartSize() {
        activePart.size = (PartSize) partSizeDropdown.value;
        UpdatePartStrings();
    }
}
