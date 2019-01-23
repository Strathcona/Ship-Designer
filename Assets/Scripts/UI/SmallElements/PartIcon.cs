using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    public void DisplayPart(Part p) {
        Clear();
        part = p;
        text.text = part.modelName;
        image.sprite = part.sprite;
        image.color = Constants.PartColor[part.partType];
        numberText.text = "S" + part.Size.ToString();
        numberTextBG.SetActive(true);
    }

    public void Clear() {
        part = null;
        text.text = "";
        numberText.text = "";
        image.sprite = defaultImage;
        numberTextBG.SetActive(false);
    }
}
