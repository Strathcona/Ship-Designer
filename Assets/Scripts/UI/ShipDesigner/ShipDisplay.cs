using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDisplay : MonoBehaviour{
    public Ship ship;
    public ShipHullsizeDisplay hullsizeDisplay;
    public ShipPartDisplay shipPartDisplay;
    public ShipSummaryDisplay shipSummaryDisplay;

    private void Awake() {
        hullsizeDisplay = GetComponentInChildren<ShipHullsizeDisplay>();
        shipPartDisplay = GetComponentInChildren<ShipPartDisplay>();
        shipSummaryDisplay = GetComponentInChildren<ShipSummaryDisplay>();
    }
    void Start() {
        DisplayRandomShip();
    }

    private void DisplayShip(Ship ship) {
        hullsizeDisplay.DisplayShip(ship);
        shipPartDisplay.DisplayShip(ship);
        shipSummaryDisplay.DisplayShip(ship);
    }

    public void DisplayRandomShip() {
        DisplayShip(Ship.MakeUpARandomShip());
    }
}
