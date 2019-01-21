using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HardpointDisplay : MonoBehaviour {
    public Hardpoint hardpoint;
    public Sprite defaultImage;
    public Image partImage;
    public Text allowableTypesText;
    public Text allowableSizeText;
    public Text partNameText;
    public GameObject partNameBackground;


    public void Awake() {
        if (defaultImage == null) {
            SpriteLoader.GetPartSprite("defaultSmall");
        }
        Clear();
    }

    public void DisplayHardpoint(Hardpoint h) {
        Clear();
        hardpoint = h;
        if(hardpoint.part == null) {
            allowableTypesText.gameObject.SetActive(true);
            allowableSizeText.gameObject.SetActive(true);
            allowableTypesText.text = hardpoint.allowableType.ToString();
            allowableSizeText.text = hardpoint.allowableSize.ToString();
        } else {
            partNameBackground.SetActive(true);
            partNameText.gameObject.SetActive(true);
            partImage.gameObject.SetActive(true);
            partImage.sprite = hardpoint.part.littleSprite;
            partImage.color = Constants.PartColor[hardpoint.part.partType];
        }
    }

    public void Clear() {
        hardpoint = null;
        partImage.sprite = defaultImage;
        partImage.gameObject.SetActive(false);
        partNameBackground.SetActive(false);
        partNameText.gameObject.SetActive(false);
        allowableTypesText.gameObject.SetActive(false);
        allowableSizeText.gameObject.SetActive(false);
    }
}