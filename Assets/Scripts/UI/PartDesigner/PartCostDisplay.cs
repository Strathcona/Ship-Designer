using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartCostDisplay : MonoBehaviour
{
    public Text text;
    public Part part;
    private void Start() {
        if (text == null) {
            text = GetComponent<Text>();
            if (text == null) {
                Debug.LogError("Part Cost Display couldn't find Text on " + gameObject.name);
            }
        }
    }

    public void DisplayPart(Part part) {
        if (part != null) {
            part.OnPartChangeEvent -= UpdateDisplay;
        }
        this.part = part;
        if (part == null) {
            text.text = "";
        } else {
            text.text = part.GetCostString();
            part.OnPartChangeEvent += UpdateDisplay;
        }
    }

    public void UpdateDisplay(Part part) {
        if (part == null) {
            text.text = "";
        } else {
            text.text = part.GetCostString();
        }
    }
}
