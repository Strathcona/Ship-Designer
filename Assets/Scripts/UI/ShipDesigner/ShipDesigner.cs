using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipDesigner : MonoBehaviour {
    public Ship ship;
    public int size;
    public InputField shipClassName;
    public InputFieldIncrement shipSize;
    public InputFieldIncrement shipLifeSupport;
    public ShipHullsizeDisplay hullsizeDisplay;
    public ShipPartSelector shipPartSelector;

    public void Awake() {
        shipClassName.gameObject.SetActive(false);
        shipSize.gameObject.SetActive(false);
        shipLifeSupport.gameObject.SetActive(false);
        hullsizeDisplay.Clear();
        LoadShip(Ship.MakeUpARandomShip());
    }

    public void UpdateHullSize() {
        hullsizeDisplay.DisplayShip(ship);
    }

    public void AskToLoadShip() {

    }

    public void LoadShip(Ship s) {
        ship = s;
        shipClassName.gameObject.SetActive(true);
        shipSize.gameObject.SetActive(true);
        shipLifeSupport.gameObject.SetActive(true);

        shipClassName.text = ship.className;
        shipSize.FieldValue = ship.hullSize;
        shipLifeSupport.FieldValue = ship.lifeSupportSize;
        shipPartSelector.DisplayShipParts(ship);
        hullsizeDisplay.DisplayShip(ship);
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
        shipSize.gameObject.SetActive(true);
        shipLifeSupport.gameObject.SetActive(true);
    }

    public void AskToSubmitShip() {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
"Would you like to submit this ship for design? It will take " + TimeManager.GetTimeString(ship.minutesToDevelop) + ".",
new List<string>() { "Yes", "No" },
new List<Action>() { SubmitDesign });

        //refresh part list
    }

    public void SubmitDesign() {

    }

    public void UpdateShipClassName() {
        ship.className = shipClassName.text;
        UpdateHullSize();
    }

    public void UpdateShipSize() {
        ship.hullSize = shipSize.FieldValue;
        UpdateHullSize();
    }

    public void UpdateShipLifeSupport() {
        ship.lifeSupportSize = shipLifeSupport.FieldValue;
        UpdateHullSize();
    }

}
