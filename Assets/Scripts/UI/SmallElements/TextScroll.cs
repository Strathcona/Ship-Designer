using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{
    public GameObject textBoxPrefab;
    public List<GameObject> textBoxes = new List<GameObject>();
    public Color leftColor = Color.blue;
    public Color rightColor = Color.green;
    public Sprite left;
    public Sprite right;

    public void DisplayMessage(string text, bool isLeft) {
        GameObject g = Instantiate(textBoxPrefab) as GameObject;
        g.transform.SetParent(transform); //want it first in the list
        g.transform.SetAsFirstSibling();
        RectTransform rt = g.GetComponent<RectTransform>();
        rt.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        //our parent rect is 180 rotated so that the vertical layout group overflows upwards
        textBoxes.Add(g);
        Image i = g.GetComponent<Image>();
        if (isLeft) {
            i.sprite = left;
            i.color = leftColor;
        } else {
            i.sprite = right;
            i.color = rightColor;
        }
        g.GetComponentInChildren<Text>().text = text;

    }

    public void Clear() {
        GameObject[] toDestroy = textBoxes.ToArray();
        for(int i = 0; i < toDestroy.Length; i++) {
            GameObject.Destroy(toDestroy[i]);
        }
        textBoxes.Clear();
    }
}
