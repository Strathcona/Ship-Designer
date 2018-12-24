using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PartDesigner : MonoBehaviour {
    public Part activePart;
    public PartDesignFieldEntry activeFieldEntry;
    public List<Part> allParts = new List<Part>();
    public List<PartDesignFieldEntry> allFieldEntries = new List<PartDesignFieldEntry>();
    public Dictionary<PartType, PartDesignFieldEntry> FieldEntryByPartType = new Dictionary<PartType, PartDesignFieldEntry>();

    public Dropdown partTierDropdown;
    public Dropdown partTypeDropdown;
    public BidList bidList;
    public Text descriptionDisplay;
    public Text statisticsDisplay;
    public InputField modelNameInput;

    private void Awake() {
        foreach(PartDesignFieldEntry p in GetComponentsInChildren<PartDesignFieldEntry>(true)) {
            p.SetUpdateStringsCallback(new Action(UpdatePartStrings));
            p.Initialize();
            allParts.Add(p.GetPart());
            allFieldEntries.Add(p);
            FieldEntryByPartType.Add(p.GetPart().partType, p);
            p.gameObject.SetActive(false);
        }
        SetActivePart();
        UpdatePartTier();
    }

    public void ResetPart() {
        activeFieldEntry.Clear();
    }

    public void UpdatePartModel() {
        activePart.partModelName = modelNameInput.text;
        UpdatePartStrings();
    }


    public void UpdatePartStrings() {
        if(activePart != null) {
            descriptionDisplay.text = activePart.GetDescriptionString();
            statisticsDisplay.text = activePart.GetStatisticsString();
        }
    }

    public void SetActivePart() {
        foreach(PartDesignFieldEntry p in allFieldEntries) {
            p.Clear();
            p.gameObject.SetActive(false);
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

    private void ActivateFieldEntry(PartType p) {
        activeFieldEntry = FieldEntryByPartType[p];
        activeFieldEntry.Clear();
        activePart = activeFieldEntry.GetPart();
        activeFieldEntry.gameObject.SetActive(true);
        bidList.activePart = activePart;
    }

    public void UpdatePartTier() {
        switch (partTierDropdown.value) {
            case 0:
                activePart.Tier = 0;
                break;
            case 1:
                activePart.Tier = 1;
                break;
            case 2:
                activePart.Tier = 2;
                break;
            case 3:
                activePart.Tier = 3;
                break;
            case 4:
                activePart.Tier = 4;
                break;
            case 5:
                activePart.Tier = 5;
                break;
            case 6:
                activePart.Tier = 6;
                break;
            default:
                Debug.Log("Fell through on tier choice ");
                break;
        }
        UpdatePartStrings();
    }
}
