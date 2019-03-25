using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PartDescriptionDisplay : MonoBehaviour, IDisplaysPart
{
    public Text text;
    public Part part;
    private void Awake() {
        if (text == null) {
            text = GetComponent<Text>();
            if (text == null) {
                Debug.LogError("Part Description Display couldn't find Text on " + gameObject.name);
            }
        }
    }

    public void DisplayPart(Part part) {
        if(part != null) {
            part.OnPartChangeEvent -= UpdateDisplay;
        }
        this.part = part;
        if(part == null) {
            text.text = "";
        } else {
            text.text = part.GetDescriptionString();
            part.OnPartChangeEvent += UpdateDisplay;
        }
    }

    public void UpdateDisplay(Part part) {
        if (part == null) {
            text.text = "";
        } else {
            text.text = part.GetDescriptionString();
        }
    }
}
