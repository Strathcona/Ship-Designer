using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class TweakableEditor : MonoBehaviour {
    public GameObject tweakableEditorPanelPrefab;
    public PartDesigner partDesigner;
    public List<GameObject> tweakableEditorPanels = new List<GameObject>();

    private void Awake() {
        Debug.Log("Loading Tweakable Editor Panel Prefab");
        tweakableEditorPanelPrefab = Resources.Load("Prefabs/Tweakable Editor Panel", typeof(GameObject)) as GameObject;
    }

    public void DisplayTweakables(Part p) {
        Clear();

        if (tweakableEditorPanels.Count < p.tweakables.Count) {
            int neededTweakableEditorPanels = p.tweakables.Count - tweakableEditorPanels.Count;
            Debug.Log("Needed tweakable editor panels " + neededTweakableEditorPanels);
            for (int i = 0; i <= neededTweakableEditorPanels; i++) {
                GameObject g = Instantiate(tweakableEditorPanelPrefab);
                g.GetComponent<TweakableEditorPanel>().partDesignerUpdateStrings = partDesigner.UpdatePartStrings;
                g.transform.SetParent(this.transform);
                g.SetActive(false);
                tweakableEditorPanels.Add(g);
            }
        }
        int index = 0;
        foreach (Tweakable t in p.tweakables) {
            tweakableEditorPanels[index].SetActive(true);
            tweakableEditorPanels[index].GetComponent<TweakableEditorPanel>().DisplayTweakable(t);
            index += 1;
        }
    }

    public void Clear() {
        for (int i = 0; i < tweakableEditorPanels.Count; i++) {
            tweakableEditorPanels[i].GetComponent<TweakableEditorPanel>().Clear();
            tweakableEditorPanels[i].SetActive(false);
        }
    }


}
