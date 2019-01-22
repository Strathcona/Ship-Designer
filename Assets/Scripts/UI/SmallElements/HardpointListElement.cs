using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HardpointListElement : MonoBehaviour
{
    public Button deleteButton;
    public Text hardpointText;
    public Hardpoint hardpoint;

    public void DisplayHardpoint(Hardpoint h) {
        Clear();
        hardpoint = h;
        string hardpointString = "Size " + hardpoint.allowableSize.ToString() + " ";
        if (h.orientation == Orientation.Internal) {
            hardpointString += "Internal " + Constants.ColoredPartTypeString[hardpoint.allowableType] + " Mount";
        } else {
            hardpointString += hardpoint.orientation.ToString() + " " + Constants.ColoredPartTypeString[hardpoint.allowableType] + " Hardpoint";
        }
        hardpointText.text = hardpointString;
    }

    public void Clear() {
        hardpointText.text = "";
        hardpoint = null;
    }
}
