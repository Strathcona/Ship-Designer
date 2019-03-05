using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HardpointDisplay : MonoBehaviour {
    public Hardpoint hardpoint;
    public bool displayPart = true;
    public Image partImage;
    public Text allowableTypesText;
    public Text allowableSizeText;
    public Text partNameText;
    public GameObject partNameBackground;


    public void Awake() {
        Clear();
    }

    public void DisplayHardpoint(Hardpoint h) {
        Clear();
        hardpoint = h;
        if(hardpoint.part == null || displayPart == false) {
            allowableTypesText.gameObject.SetActive(true);
            allowableSizeText.gameObject.SetActive(true);
            partImage.gameObject.SetActive(false);
            partNameBackground.SetActive(false);
            partNameText.gameObject.SetActive(false);
            allowableTypesText.text = Constants.ColoredPartTypeString[hardpoint.allowableType];
            allowableSizeText.text = hardpoint.allowableSize.ToString();
        } else {
            allowableTypesText.gameObject.SetActive(false);
            allowableSizeText.gameObject.SetActive(false);
            partNameBackground.SetActive(true);
            partNameText.gameObject.SetActive(true);
            partImage.gameObject.SetActive(true);
            partNameText.text = hardpoint.part.ModelName;
            partImage.sprite = hardpoint.part.sprite;
            partImage.color = Constants.PartColor[hardpoint.part.partType];
        }
    }

    public void Refresh() {
        if (hardpoint.part == null || displayPart == false) {
            allowableTypesText.gameObject.SetActive(true);
            allowableSizeText.gameObject.SetActive(true);
            partImage.gameObject.SetActive(false);
            partNameBackground.SetActive(false);
            partNameText.gameObject.SetActive(false);
            allowableTypesText.text = Constants.ColoredPartTypeString[hardpoint.allowableType];
            allowableSizeText.text = hardpoint.allowableSize.ToString();
        } else {
            allowableTypesText.gameObject.SetActive(false);
            allowableSizeText.gameObject.SetActive(false);
            partNameBackground.SetActive(true);
            partNameText.gameObject.SetActive(true);
            partImage.gameObject.SetActive(true);
            partNameText.text = hardpoint.part.ModelName;
            partImage.sprite = hardpoint.part.sprite;
            partImage.color = Constants.PartColor[hardpoint.part.partType];
        }
    }

    public void Clear() {
        hardpoint = null;
        partImage.sprite = null;
        partImage.gameObject.SetActive(false);
        partNameBackground.SetActive(false);
        partNameText.gameObject.SetActive(false);
        allowableTypesText.gameObject.SetActive(false);
        allowableSizeText.gameObject.SetActive(false);
    }
}