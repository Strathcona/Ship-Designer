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
    public GameObject fadePanel;
    public Text size;
    public Text tier;
    public Text power;
    public Button button;
    public Outline outline;

    public void Awake() {
        smallTweakableDisplayPrefab = Resources.Load("Prefabs/Small Tweakable Display", typeof(GameObject)) as GameObject;
        tweakableDisplayPool = new GameObjectPool(smallTweakableDisplayPrefab, tweakableRoot);
        bgImage = GetComponent<Image>();
    }

    public void SetOutLine(bool showOutline) {
        outline.enabled = showOutline;
    }

    public void DisplayPart(Part p) {
        Clear();
        part = p;
        bgImage.color = Constants.PartColor[part.partType];
        nameText.text = part.GetDescriptionString();
        size.text = "Size:" + part.Size.ToString();
        tier.text = "T" + part.Tier.ToString();
        power.text = "Power" + part.NetPower.ToString();
        foreach(Tweakable t in part.tweakables) {
            GameObject g = tweakableDisplayPool.GetGameObject();
            g.GetComponent<SmallTweakableDisplay>().DisplayTweakable(t);
        }
    }

    public void Clear() {
        part = null;
        tweakableDisplayPool.ReleaseAll();
        nameText.text = "";
        size.text = "";
        power.text = "";
        bgImage.color = Color.white;
    }
}
