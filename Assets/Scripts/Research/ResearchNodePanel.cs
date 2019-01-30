using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;
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
        node.onChange = Refresh;
        text.text = node.name;
        fade.fillAmount = 1;
        if (node.effect.Contains("Weapon")) {
            icon.color = Constants.PartColor[PartType.Weapon];
        } else if(node.effect.Contains("Sensor")) {
            icon.color = Constants.PartColor[PartType.Sensor];
        } else if (node.effect.Contains("FireControl")) {
            icon.color = Color.blue;
        } else if (node.effect.Contains("Reactor")) {
            icon.color = Constants.PartColor[PartType.Reactor];
        } else if (node.effect.Contains("Engine")) {
            icon.color = Constants.PartColor[PartType.Engine];
        } else if (node.nodeType == ResearchNodeType.Start) {
            icon.color = Color.green;
        } else if (node.nodeType == ResearchNodeType.End) {
            icon.color = Color.red;
        } else {
            icon.color = Color.cyan;
        }
        
        if (node.Active) {
            outline.gameObject.SetActive(true);
        } else {
            outline.gameObject.SetActive(false);
        }
    }

    public void Refresh() {
        fade.fillAmount = (float)(node.cost - node.Progress) / node.cost;
        if (node.Active) {
            outline.gameObject.SetActive(true);
        } else {
            outline.gameObject.SetActive(false);
        }
    }
}
