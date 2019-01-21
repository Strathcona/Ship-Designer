using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipDesigner : MonoBehaviour {
    public Ship ship;
    public int size;
    public InputField shipClassName;
    public ShipPartSelector shipPartSelector;
    public HardpointEditor hardpointEditor;
    public HardpointLayout hardpointLayout;

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
    "Are you sure you want to edit this ships's mounts and hardpoints? If you change hardpoints or mounts, all attached parts will be removed from the ship",
    new List<string>() { "Yes", "No" },
    new List<Action>() { EditHardpoints });
    }

    public void EditHardpoints() {
        hardpointEditor.gameObject.SetActive(true);
        Debug.Log(ship.hardpoints.Count);
        hardpointEditor.LoadHardpoints(ship.hardpoints);
    }

    public void FinishedEditingHardpoints() {
        hardpointEditor.gameObject.SetActive(false);
        ship.SetHardpoints(new List<Hardpoint>(hardpointEditor.hardpoints));
        hardpointLayout.DisplayHardpoints(ship.hardpoints);
        Debug.Log(ship.hardpoints.Count);
    }

    public void CancelEditingHardpoints() {
        hardpointEditor.gameObject.SetActive(false);
    }

    public void LoadShip(Ship s) {
        ship = s;
        shipClassName.gameObject.SetActive(true);

        shipClassName.text = ship.className;
        shipPartSelector.DisplayShipParts(ship);
    }

    public void Clear() {

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
