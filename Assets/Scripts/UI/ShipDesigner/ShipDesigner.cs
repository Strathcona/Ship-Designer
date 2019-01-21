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

    public void Awake() {
        shipClassName.gameObject.SetActive(false);
        LoadShip(Ship.MakeUpARandomShip());
    }

    public void AskToLoadShip() {

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
