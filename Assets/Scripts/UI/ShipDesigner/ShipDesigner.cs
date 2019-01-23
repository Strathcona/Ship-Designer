using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class ShipDesigner : MonoBehaviour {
    public Ship ship;
    public int size;
    public InputField shipClassName;
    public PartSelector partSelector;
    public Text summaryText;
    public Button viewSummary;
    public HardpointEditor hardpointEditor;
    public HardpointLayout hardpointLayout;
    public HardpointLayout hardpointLayoutParts;

    public void Awake() {
        shipClassName.gameObject.SetActive(false);
        hardpointEditor.finishedEditingButton.onClick.AddListener(FinishedEditingHardpoints);
        hardpointEditor.cancelButton.onClick.AddListener(CancelEditingHardpoints);
        hardpointLayout.DisplayHardpoints(ship.hardpoints);
    }

    public void AskToLoadShip() {
        LoadShip(Ship.MakeUpARandomShip());
    }

    public void AskToEditHardpoints() {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
    "Are you sure you want to edit this ships's mounts and hardpoints? If you change hardpoints or mounts, attached parts will be removed from the ship",
    new List<string>() { "Yes", "No" },
    new List<Action>() { EditHardpoints });
    }

    public void EditHardpoints() {
        hardpointEditor.gameObject.SetActive(true);
        hardpointEditor.LoadHardpoints(ship.hardpoints);
    }

    public void FinishedEditingHardpoints() {
        hardpointEditor.gameObject.SetActive(false);
        ship.SetHardpoints(new List<Hardpoint>(hardpointEditor.GetHardpoints()));
        hardpointLayout.DisplayHardpoints(ship.hardpoints);
        hardpointLayoutParts.DisplayHardpoints(ship.hardpoints);
    }

    public void CancelEditingHardpoints() {
        hardpointEditor.gameObject.SetActive(false);
    }

    public void EditParts() {
        partSelector.gameObject.SetActive(true);
        partSelector.LoadShip(ship);
    }

    public void FinishedEditingParts() {
        partSelector.gameObject.SetActive(false);
        hardpointLayoutParts.DisplayHardpoints(ship.hardpoints);
    }

    public void LoadShip(Ship s) {
        ship = s;
        shipClassName.gameObject.SetActive(true);
        shipClassName.text = ship.className;
        hardpointLayout.DisplayHardpoints(ship.hardpoints);
        hardpointLayoutParts.DisplayHardpoints(ship.hardpoints);
    }

    public void Clear() {
        ship = null;
        hardpointLayout.Clear();
        hardpointLayoutParts.Clear();
    }


    public void AskToCreateShip() {
            ModalPopupManager.instance.DisplayModalPopup("Confirmation",
    "Are you sure you want to design a new Ship?",
    new List<string>() { "Yes", "No" },
    new List<Action>() { CreateNewShip});
    }

    public void CreateNewShip() {
        Clear();
        ship = new Ship();
        shipClassName.gameObject.SetActive(true);
    }

    public void AskToSubmitShip() {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
"Would you like to submit this ship for design? It will take " + TimeManager.GetTimeString(ship.minutesToDesign) + ".",
new List<string>() { "Yes", "No" },
new List<Action>() { SubmitDesign });

        //refresh part list
    }

    public void SubmitDesign() {

    }

    public void UpdateShipClassName() {
        ship.className = shipClassName.text;
    }

    public void UpdateShipStatistics() {
        Dictionary<PartSize, int> crewPerHardpoint = new Dictionary<PartSize, int>() {
            { PartSize.XS, 1 },
            { PartSize.S, 2 },
            { PartSize.M, 4 },
            { PartSize.L, 8 },
            { PartSize.XL, 16 },
        };

        ship.classificaiton = CalculateClassification(ship.hardpoints);
        if(ship.classificaiton == ShipType.Fighter) {
            ship.crew = 1;
        } else {
            foreach(Hardpoint h in ship.hardpoints) {
                ship.crew += crewPerHardpoint[h.allowableSize];
            }
            ship.crew = Mathf.CeilToInt(ship.crew * 1.1f);
        }

    }

    public ShipType CalculateClassification(List<Hardpoint> hardpoints) {
        int xs = hardpoints.FindAll(h => h.allowableSize == PartSize.XS).Count;
        int s = hardpoints.FindAll(h => h.allowableSize == PartSize.S).Count;
        int m = hardpoints.FindAll(h => h.allowableSize == PartSize.M).Count;
        int l = hardpoints.FindAll(h => h.allowableSize == PartSize.L).Count;
        int xl = hardpoints.FindAll(h => h.allowableSize == PartSize.XL).Count;
        if(xs == 0 && s == 0 && m ==0 && l == 0 && xl == 0) {
            return ShipType.None;
        }
        if(s == 0 && m ==0 && l == 0 && xl == 0) {
            //only xs
            if(xs > 15) {
                if(ship.hardpoints.FindAll(h => h.allowableType == PartType.Weapon).Count > 0) {
                    //got a bunch of xs slots and weapons
                    return ShipType.Gunboat;
                } else {
                    //got a bunch of xs slots but no weapons
                    return ShipType.Utility;
                }
            } else {
                //only a few slots 
                if (ship.hardpoints.FindAll(h => h.allowableType == PartType.Weapon).Count > 0) {
                    return ShipType.Fighter;
                } else {
                    return ShipType.Patrol;
                }
            }
        }
        if(m == 0 && l == 0 && xl == 0) {
            if(xs + s > 15) {
                return ShipType.LightCruiser;
            } else {
                return ShipType.Destroyer;
            }
        }
        if(l == 0 && xl == 0) {
            if (xs + s + m > 40) {
                return ShipType.Battlecruiser;
            } else if (xs + s + m > 20) {
                return ShipType.HeavyCruiser;
            } else {
                return ShipType.LightCruiser;
            }
        }
        if(xs + s + m + l + xl > 40) {
            return ShipType.Battleship;
        } else {
            return ShipType.Battlecruiser;
        }
    }
}
