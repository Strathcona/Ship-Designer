using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallTweakableDisplay : MonoBehaviour
{
    public Tweakable tweakable;
    public Text text;


    public void DisplayTweakable(Tweakable t) {
        Clear();
        tweakable = t;
        text.text = tweakable.tweakableName + ":" + tweakable.Value.ToString();
        text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, LayoutUtility.GetPreferredWidth(text.rectTransform));
    }

    public void Clear() {
        tweakable = null;
        text.text = "";
    }
}
