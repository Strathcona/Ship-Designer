using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;
using UnityEngine.UI;

public class ShipPartSelector : MonoBehaviour {
    public Event onPartsSelected;
    public PartType partType;
    public Text buttonText;

    private void Awake() {
        switch(partType){
            case PartType.Engine:
                buttonText.text = "Select Engines";
                break;
            case PartType.FireControl:
                buttonText.text = "Select Fire Controls";
                break;
            case PartType.PowerPlant:
                buttonText.text = "Select Power Plants";
                break;
            case PartType.Sensor:
                buttonText.text = "Select Sensors";
                break;
            case PartType.Weapon:
                buttonText.text = "Select Weapons";
                break;
        }
    }
}
