using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PartIcon : MonoBehaviour
{
    public Part part;
    public Sprite defaultImage;
    public Image image;
    public Text text;
    public GameObject numberTextBG;
    public Text numberText;


    public void Awake() {
        if(defaultImage == null) {
            SpriteLoader.GetPartSprite("defaultSmall");
        }
        Clear();
    }

    public void DisplayPart(Part p, int number=1) {
        Clear();
        part = p;
        text.text = part.modelName;
        image.sprite = part.littleSprite;
        image.color = Constants.PartColor[part.partType];
        if(number > 1) {
            numberText.text = "x" + number.ToString();
            numberTextBG.SetActive(true);
        }
    }

    public void Clear() {
        part = null;
        text.text = "";
        numberText.text = "";
        image.sprite = defaultImage;
        numberTextBG.SetActive(false);
    }
}
