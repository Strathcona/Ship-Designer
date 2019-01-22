using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipDesigner : MonoBehaviour {
    public Ship ship;
    public int size;
    public InputField shipClassName;
    public PartSelector partSelector;
    public HardpointEditor hardpointEditor;
    public HardpointLayout hardpointLayout;
    public HardpointLayout hardpointLayoutParts;

    public void Awake() {
        shipClassName.gameObject.SetActive(false);
        LoadShip(Ship.MakeUpARandomShip());
        hardpointEditor.finishedEditingButton.onClick.AddListener(FinishedEditingHardpoints);
        hardpointEditor.cancelButton.onClick.AddListener(CancelEditingHardpoints);
        hardpointLayout.DisplayHardpoints(ship.hardpoints);
    }

    public void AskToLoadShip() {

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
}
