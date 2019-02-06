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
    public Image manufacturerImage;
    public GameObject manufacturerDisplay;
    public ManufacturerEditor manufacturerEditor;
    public Text manufacturerButtonText;
    public Button manufacturerEditButton;

    public void Start() {
        createNewDropdown.ClearOptions();
        createNewDropdown.options.Add(new Dropdown.OptionData("Create New"));
        foreach (PartType t in Enum.GetValues(typeof(PartType))) {
            createNewDropdown.options.Add(new Dropdown.OptionData(Constants.ColoredPartTypeString[t]));
        }
        createNewDropdown.value = 0;
        createNewDropdown.RefreshShownValue();
        ToggleVisible(false);
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
        string key = Constants.PartTypeString[p.partType] + " MaxTier";
        int maxTier = ResearchManager.instance.GetResearchValue(key);
        if (maxTier < p.Tier) {
            ModalPopupManager.instance.DisplayModalPopup("Unable to Edit", "The Part your are trying to load is too advanced for us to modify it. Increase your " + Constants.ColoredPartTypeString[p.partType] + " technology in order to make changes to this part.", "Okay");
        } else {
            ToggleVisible(true);
            Debug.Log("Loading Part" + p.modelName + " " + p.Tier);
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
                case PartType.Shield:
                    activePart = new Shield();
                    break;
            }
            activePart = p.Clone();
            previewImage.sprite = activePart.sprite;
            modelNameInput.text = activePart.modelName;
            SetPartModel();
            UpdateTierDropdown();
            partTierDropdown.value = activePart.Tier;
            tweakableEditor.DisplayTweakables(activePart);
            DisplayManufacturer();
        }

    }

    public void Clear() {
        activePart = null;
        tweakableEditor.Clear();
        descriptionDisplay.text = "---";
        statisticsDisplay.text = "---";
        ToggleVisible(false);
        DisplayManufacturer();
    }

    public void CreateNewPart() {
        Clear();
        ToggleVisible(true);

        switch (createNewDropdown.value) {
            case 0:
                //this is the first value, the "Create New Part" value
                break;
            //the rest are in the order as defined in the Enum
            //Weapon, FireControl, Sensor, Engine, Reactor, Shield 
            case 1:
                activePart = new Weapon();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultWeaponS");
                break;
            case 2:
                activePart = new FireControl();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultFireControlS");
                break;
            case 3:
                activePart = new Sensor();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultSensorS");
                break;
            case 4:
                activePart = new Engine();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultEngineS");
                break;
            case 5:
                activePart = new Reactor();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultReactorS");
                break;
            case 6:
                activePart = new Shield();
                previewImage.sprite = SpriteLoader.GetPartSprite("defaultShieldS");
                break;

        }
        activePart.sprite = previewImage.sprite;
        createNewDropdown.value = 0;
        tweakableEditor.DisplayTweakables(activePart);
        UpdateTierDropdown();

    }

    public void ResetCreateNewDropdown() {
        createNewDropdown.value = 0;
    }

    public void UpdateTierDropdown() {
        if(activePart != null) {
            partTierDropdown.ClearOptions();
            string key = Constants.PartTypeString[activePart.partType] + " MaxTier";
            int maxTier = ResearchManager.instance.GetResearchValue(key);

            for (int tier = 0; tier < maxTier; tier++) {
                partTierDropdown.options.Add(new Dropdown.OptionData("Tier " + tier.ToString()));
            }
            partTierDropdown.value = 0;
            partTierDropdown.RefreshShownValue();
            
        }
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
        if (activePart != null) {
            ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Would you like to submit this part for design? It will take " + TimeManager.GetTimeString(activePart.minutesToDevelop) + ".",
            new List<string>() { "Yes", "No" },
            new List<Action>() { SubmitDesign });
            //refresh part list
        }
    }

    public void DisplayManufacturer() {
        if(activePart != null) {
            if (activePart.manufacturer != null) {
                manufacturerImage.gameObject.SetActive(true);
                manufacturerImage.sprite = activePart.manufacturer.logo;
                manufacturerButtonText.text = activePart.manufacturer.name + "\n" + "(Change)";
            } else {
                manufacturerImage.gameObject.SetActive(false);
                manufacturerButtonText.text = "Select\nManufacturer";
            }
        } else {
            manufacturerImage.gameObject.SetActive(false);
            manufacturerButtonText.text = "Select\nManufacturer";
        }

    }

    public void EditManufacturer() {
        manufacturerEditor.gameObject.SetActive(true);
        manufacturerEditor.LoadPart(activePart);
    }

    public void FinishedEditingManufacturer() {
        activePart.manufacturer = manufacturerEditor.company;
        manufacturerEditor.gameObject.SetActive(false);
        DisplayManufacturer();
    }

    public void SubmitDesign() {
        PartLibrary.AddPartToDevelopment(activePart);
    }

    public void UpdatePartTier() {
        activePart.Tier = partTierDropdown.value;
        UpdatePartStrings();
    }

    public void UpdatePartSize() {
        activePart.Size = (PartSize) partSizeDropdown.value;
        UpdatePartStrings();
    }

    public void ToggleVisible(bool on) {
        modelNameInput.gameObject.SetActive(on);
        partTierDropdown.gameObject.SetActive(on);
        previewImage.gameObject.SetActive(on);
        partSizeDropdown.gameObject.SetActive(on);
        manufacturerEditButton.gameObject.SetActive(on);
    }
}
