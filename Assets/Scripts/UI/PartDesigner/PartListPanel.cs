using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PartListPanel : MonoBehaviour {
    public Text topText;
    public Text bottomText;
    public Part part;
    public PartList partList;

    public void DisplayPart(Part p) {
        Clear();
        part = p;
        topText.text = p.modelName + " tier "+ p.Tier.ToString()+ " " + Constants.PartTypeString[p.partType];
        bottomText.text = p.ticksToDesign.ToString();
    }

    public void Clear() {
        part = null;
        topText.text = "";
        bottomText.text = "";
    }

    public void EditPart() {
        partList.EditPart(part);
    }
    
}
