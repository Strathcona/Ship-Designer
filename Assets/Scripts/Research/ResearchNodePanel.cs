using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchNodePanel : MonoBehaviour {
    public ResearchNode node;
    public Text text;
    public Image fade;
    public Image icon;
    public Image background;
    public Image outline;

    private void Awake() {
        outline.color = Color.green;
    }

    public void DisplayResearchNode(ResearchNode _node) {
        node = _node;
        text.text = node.name;
        fade.fillAmount = 1;
        if (node.active) {
            outline.gameObject.SetActive(true);
        } else {
            outline.gameObject.SetActive(false);
        }
    }

    public void Refresh() {
        fade.fillAmount = (float)(node.cost - node.progress) / node.cost;
        if (node.active) {
            outline.gameObject.SetActive(true);
        } else {
            outline.gameObject.SetActive(false);
        }
    }
}
