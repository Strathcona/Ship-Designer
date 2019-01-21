using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSummaryDisplay : MonoBehaviour {
    public Ship ship;
    private Text summaryText;

    private void Awake() {
        summaryText = GetComponentInChildren<Text>();
    }

    public void DisplayShip(Ship s) {
        ClearDisplay();
        ship = s;
        string firstline = s.shipName + ", " + s.className + " class Cruiser\n";
        string secondline = "Her total hull size is <b>" + s.hull.ToString() + "</b>\n";
        string thirdline = "Her speed rating is " + s.speedRating.ToString() + " and her maneoverability is " + s.manevorability.ToString()+"\n";
        string fourthline = "Her first strike damage is " + s.alphaDamage.ToString() + " while her average damage is " + s.averageDamage.ToString();
        summaryText.text = firstline + secondline + thirdline + fourthline;
    }

    public void ClearDisplay() {
        summaryText.text = "";
        ship = null;
    }

}
