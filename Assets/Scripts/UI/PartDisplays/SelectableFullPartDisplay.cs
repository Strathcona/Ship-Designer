using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;
using System;

public class SelectableFullPartDisplay : MonoBehaviour
{
    public Part part;
    public Image bgImage;
    public Text nameText;
    public GameObject tweakableRoot;
    public GameObjectPool tweakableDisplayPool;
    public List<GameObject> tweakableDisplays = new List<GameObject>();
    public GameObject smallTweakableDisplayPrefab;
    public GameObject overlayPanel;
    public Image overlayFill;
    public Text overlayText;
    public Text weight;
    public Text tier;
    public Text power;
    public Text developmentText;
    public Button button;
    public Outline outline;

    public void Awake() {
        smallTweakableDisplayPrefab = Resources.Load("Prefabs/Small Tweakable Display", typeof(GameObject)) as GameObject;
        tweakableDisplayPool = new GameObjectPool(smallTweakableDisplayPrefab, tweakableRoot);
        bgImage = GetComponent<Image>();
    }

    public void Select() {
        outline.enabled = true;
    }

    public void Deselect() {
        outline.enabled = false;
    }

    public void DisplayPart(Part p) {
        Clear();
        part = p;
        bgImage.color = Constants.PartColor[part.partType];
        nameText.text = part.GetDescriptionString();
        weight.text = "Weight:" + part.Weight.ToString();
        tier.text = "T" + part.Tier.ToString();
        power.text = "Power" + part.NetPower.ToString();
        foreach(Tweakable t in part.tweakables) {
            GameObject g = tweakableDisplayPool.GetGameObject();
            g.GetComponent<SmallTweakableDisplay>().DisplayTweakable(t);
        }
        if (!part.IsDesigned) {
            overlayPanel.SetActive(true);
            overlayText.text = "IN DEVELOPMENT";
        } else {
            overlayPanel.SetActive(false);
        }
    }

    public void Clear() {
        part = null;
        tweakableDisplayPool.ReleaseAll();
        nameText.text = "";
        weight.text = "";
        power.text = "";
        bgImage.color = Color.white;
        overlayPanel.SetActive(false);
        overlayText.text = "";
    }
}
