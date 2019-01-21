using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HardpointListElement : MonoBehaviour
{
    public Button deleteButton;
    public Text hardpointText;

    public void DisplayHardpoint(Hardpoint h) {
        Clear();
        string hardpointString = "Size " + h.allowableSize.ToString() + " ";
        if (h.orientation == Orientation.Internal) {
            hardpointString += "Internal " + Constants.ColoredPartTypeString[h.allowableType] + " Mount";
        } else {
            hardpointString += h.orientation.ToString() + " " + Constants.ColoredPartTypeString[h.allowableType] + " Hardpoint";
        }
        hardpointText.text = hardpointString;
    }

    public void Clear() {
        hardpointText.text = "";
    }
}
