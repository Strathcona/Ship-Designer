using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDisplay : MonoBehaviour {
    Part partToDisplay;
    Text descriptionText;
    Text statisticsText;

    private void Awake() {
        descriptionText = this.transform.GetChild(0).GetComponentInChildren<Text>();
        statisticsText = this.transform.GetChild(1).GetComponentInChildren<Text>();

    }

    public void DisplayPart(Part p) {
        partToDisplay = p;
        descriptionText.text = p.GetDescriptionString();
        statisticsText.text = p.GetStatisticsString();
    }
}
