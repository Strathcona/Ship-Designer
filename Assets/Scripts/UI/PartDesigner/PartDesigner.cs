using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PartDesigner : MonoBehaviour {
    public Part activePart;
    public PartDesignFieldEntry activeFieldEntry;


    public List<PartDesignFieldEntry> allFieldEntries = new List<PartDesignFieldEntry>();

    public Dropdown partTierDropdown;
    public Dropdown partTypeDropdown;
    public Text descriptionDisplay;
    public Text statisticsDisplay;
    public InputField modelNameInput;

    public PartList partList;

    public Dictionary<PartType, PartDesignFieldEntry> FieldEntryByPartType = new Dictionary<PartType, PartDesignFieldEntry>();

    [SerializeField]
    public List<Part> allParts = new List<Part>() {
        new Weapon(),
        new FireControl(),
        new Sensor(),
        new PowerPlant(),
        new Engine()
    };

    public Dictionary<PartType, Part> PartByPartType;


    private void Awake() {
        PartByPartType = new Dictionary<PartType, Part>() {
        {PartType.Weapon, allParts[0] },
        {PartType.FireControl, allParts[1] },
        {PartType.Sensor, allParts[2] },
        {PartType.PowerPlant, allParts[3] },
        {PartType.Engine, allParts[4] }
        };
        partList.partDesigner = this;
        partList.DisplayParts();

        foreach(PartDesignFieldEntry p in GetComponentsInChildren<PartDesignFieldEntry>(true)) {
            p.SetUpdateStringsCallback(new Action(UpdatePartStrings));
            p.Initialize(PartByPartType[p.GetPartType()]);
            allFieldEntries.Add(p);
            FieldEntryByPartType.Add(p.GetPartType(), p);
            p.gameObject.SetActive(false);
        }
        SetActivePart();
        UpdatePartTier();
    }

    public void ResetPart() {
        activeFieldEntry.Clear();
    }

    public void UpdatePartModel() {
        activePart.modelName = modelNameInput.text;
        UpdatePartStrings();
    }


    public void UpdatePartStrings() {
        if(activePart != null) {
            descriptionDisplay.text = activePart.GetDescriptionString();
            statisticsDisplay.text = activePart.GetStatisticsString();
        }
    }

    public void LoadPart(Part p) {
        activePart = PartByPartType[p.partType];
        activePart.CopyValuesFromPart(p);
        activeFieldEntry = FieldEntryByPartType[p.partType];
        activeFieldEntry.SetPart();
        modelNameInput.text = activePart.modelName;
        UpdatePartModel();
        switch (activePart.partType) {
            case PartType.Weapon:
                partTypeDropdown.value = 0;
                break;
            case PartType.FireControl:
                partTypeDropdown.value = 1;
                break;
            case PartType.Sensor:
                partTypeDropdown.value = 2;
                break;
            case PartType.PowerPlant:
                partTypeDropdown.value = 3;
                break;
            case PartType.Engine:
                partTypeDropdown.value = 4;
                break;
            default:
                Debug.Log("Fell through on editing part setting parttype dropdown");
                break;
        }
        partTierDropdown.value = activePart.Tier;
        activeFieldEntry.gameObject.SetActive(true);
    }

    public void SetActivePart() {
        foreach(PartDesignFieldEntry pdfe in allFieldEntries) {
            pdfe.Clear();
            pdfe.gameObject.SetActive(false);
        }
        //must match the choices in the dropdown, obviously
        switch (partTypeDropdown.value) {
            case 0:
                ActivateFieldEntry(PartType.Weapon);
                break;
            case 1:
                ActivateFieldEntry(PartType.FireControl);
                break;
            case 2:
                ActivateFieldEntry(PartType.Sensor);
                break;
            case 3:
                ActivateFieldEntry(PartType.PowerPlant);
                break;
            case 4:
                ActivateFieldEntry(PartType.Engine);
                break;
            default:
                Debug.Log("Fell through on partType selection");
                break;
        }
        UpdatePartStrings();
    }

    private void ActivateFieldEntry(PartType pt) {
        activeFieldEntry = FieldEntryByPartType[pt];
        activeFieldEntry.Clear();
        activePart = PartByPartType[pt];
        activeFieldEntry.gameObject.SetActive(true);
    }

    public void UpdatePartTier() {
        activePart.Tier = partTierDropdown.value;
        UpdatePartStrings();
    }
}
