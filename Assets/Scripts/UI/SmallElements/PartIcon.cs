using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameConstructs;

public class PartIcon : MonoBehaviour
{
    public Part part;
    public Image image;
    public Text text;
    public GameObject numberTextBG;
    public Text numberText;

    public void Awake() {
        Clear();
    }

    public void DisplayPart(Part p) {
        Clear();
        part = p;
        text.text = part.ModelName;
        image.sprite = part.sprite;
        image.color = Constants.PartColor[part.partType];
        numberText.text = part.Size.ToString();
        numberTextBG.SetActive(true);
    }

    public void Clear() {
        part = null;
        text.text = "";
        numberText.text = "";
        image.sprite = null;
        numberTextBG.SetActive(false);
    }
}
