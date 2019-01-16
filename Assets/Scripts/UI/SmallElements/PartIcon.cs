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


    public void Awake() {
        if(defaultImage == null) {
            SpriteLoader.GetPartSprite("defaultSmall");
        }
        Clear();
    }

    public void DisplayPart(Part p) {
        part = p;
        text.text = part.modelName;
        image.sprite = part.littleSprite;
        image.color = Constants.PartColor[part.partType];
    }

    public void Clear() {
        part = null;
        text.text = "";
        image.sprite = defaultImage;
    }
}
