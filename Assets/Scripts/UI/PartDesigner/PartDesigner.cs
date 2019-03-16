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
    public InputField modelNameInput;
    public TweakableEditor tweakableEditor;
    public GameObject manufacturerDisplay;
    public ManufacturerEditor manufacturerEditor;
    public Text manufacturerButtonText;
    public PartManufacturerDisplay partManufacturerDisplay;
    public Button manufacturerEditButton;
    public event Action<Part> OnActivePartChangeEvent;

    public void Start() {
        createNewDropdown.ClearOptions();
        createNewDropdown.options.Add(new Dropdown.OptionData("Create New"));
        foreach (PartType t in Enum.GetValues(typeof(PartType))) {
            createNewDropdown.options.Add(new Dropdown.OptionData(Constants.ColoredPartTypeString[t]));
        }
        createNewDropdown.value = 0;
        createNewDropdown.RefreshShownValue();
        ToggleVisible(false);
        manufacturerDisplay.SetActive(false);
        Component[] components = GetComponentsInChildren(typeof(IDisplaysPart));
        for(int i = 0; i < components.Length; i++) {
            IDisplaysPart displaysPart = components[i] as IDisplaysPart;
            OnActivePartChangeEvent += displaysPart.DisplayPart;
        }
    }

    public void UpdatePartModel() {
        activePart.ModelName = modelNameInput.text;
    }

    public void SetPartModel() {
        modelNameInput.text = activePart.ModelName;
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
            Debug.Log("Loading Part" + p.ModelName + " " + p.Tier);
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
            modelNameInput.text = activePart.ModelName;
            SetPartModel();
            UpdateTierDropdown();
            partTierDropdown.value = activePart.Tier;
            ActivePartChange();
        }

    }

    public void Clear() {
        activePart = null;
        tweakableEditor.Clear();
        ToggleVisible(false);
        OnActivePartChangeEvent?.Invoke(activePart);
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
                break;
            case 2:
                activePart = new FireControl();
                break;
            case 3:
                activePart = new Sensor();
                break;
            case 4:
                activePart = new Engine();
                break;
            case 5:
                activePart = new Reactor();
                break;
            case 6:
                activePart = new Shield();
                break;

        }
        createNewDropdown.value = 0;
        UpdateTierDropdown();
        ActivePartChange();
    }

    private void ActivePartChange() {
        tweakableEditor.DisplayTweakables(activePart);
        OnActivePartChangeEvent?.Invoke(activePart);
        partManufacturerDisplay.DisplayPart(activePart);
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
            "Would you like to submit this part for development? It will take " + activePart.DesignCost + " units of design effort for your engineers to create a functional design.",
            new List<string>() { "Yes", "No" },
            new List<Action>() { SubmitDesign });
            //refresh part list
        }
    }

    public void EditManufacturer() {
        manufacturerEditor.LoadPart(activePart);
    }

    public void SubmitDesign() {
        Part p = activePart.Clone();
        PlayerManager.instance.activePlayer.ActiveCompany.engineeringDepartment.SubmitPartDesign(p);
    }

    public void UpdatePartTier() {
        activePart.Tier = partTierDropdown.value;
    }

    public void UpdatePartSize() {
        activePart.Size = (PartSize) partSizeDropdown.value;
    }

    public void ToggleVisible(bool on) {
        modelNameInput.gameObject.SetActive(on);
        partTierDropdown.gameObject.SetActive(on);
        partSizeDropdown.gameObject.SetActive(on);
        manufacturerDisplay.gameObject.SetActive(on);
    }
}
