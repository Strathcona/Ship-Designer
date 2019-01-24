using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchNodePanel : MonoBehaviour {
    public ResearchNode node;
    public Image image;
    public Image fade;
    public Text text;
    public Outline outline;

    public void DisplayResearchNode(ResearchNode _node) {
        node = _node;
        node.onUpdate = Refresh;
        text.text = node.name;
        fade.fillAmount = (float)node.cost/node.progress;
        image.color = node.nodeColor;
        if (node.active) {
            outline.enabled = true;
            outline.effectColor = Color.green;
        } else {
            outline.enabled = false;
        }
    }

    public void Refresh() {
        fade.fillAmount = (float)node.cost / node.progress;
        if (node.active) {
            outline.enabled = true;
            outline.effectColor = Color.green;
        } else {
            outline.enabled = false;
        }
    }
}
