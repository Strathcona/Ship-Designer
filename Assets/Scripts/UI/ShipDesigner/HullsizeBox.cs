using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HullsizeBox : MonoBehaviour {
    public Part displayedPart;
    private Image image;
    private Dictionary<PartType, Color> partColor = new Dictionary<PartType, Color>() {
        {PartType.Weapon, Color.red },
        {PartType.FireControl, Color.green },
        {PartType.PowerPlant, Color.yellow},
        {PartType.Sensor, Color.blue },
        {PartType.Engine, Color.magenta }
    };

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void ClearPart() {
        displayedPart = null;
        image.color = Color.white;
    }

    public void SetPart(Part p) {
        displayedPart = p;
        image.color = partColor[p.partType];
    }
}
