using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartManufacturerDisplay : MonoBehaviour, IDisplaysPart
{
    public LogoDisplay logoDisplay;
    public Part part;
    private void Start() {
        if (logoDisplay == null) {
            logoDisplay = GetComponent<LogoDisplay>();
            if (logoDisplay == null) {
                Debug.LogError("Part Manufacturer Display couldn't find LogoDisplay on " + gameObject.name);
            }
        }
    }

    public void DisplayPart(Part part) {
        if (part != null) {
            part.OnManufactuerChange -= UpdateDisplay;
        }
        this.part = part;
        if(this.part != null) {
            if(this.part.Manufacturer != null) {
                Debug.Log(part.Manufacturer.logo);
                logoDisplay.DisplayLogo(part.Manufacturer.logo);
            }
            this.part.OnManufactuerChange += UpdateDisplay;
        }
    }

    public void UpdateDisplay() {
        if (part != null) {
            if (part.Manufacturer != null) {
                logoDisplay.DisplayLogo(part.Manufacturer.logo);
            }
        }
    }
}
