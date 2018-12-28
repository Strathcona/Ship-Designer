using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartList : MonoBehaviour {
    public Transform partListPanelPrefab;
    public List<GameObject> partListPanels = new List<GameObject>();
    public PartDesigner partDesigner;

    public void EditPart(Part p) {
        partDesigner.LoadPart(p);
    }

    public void DisplayParts() {
        Clear();
        List<Part> parts = PartLibrary.GetParts();

        if (parts.Count > partListPanels.Count) {
            int neededPartListPanels = parts.Count - partListPanels.Count;
            Debug.Log("Needed partListPanels " + neededPartListPanels);
            for (int i = 0; i <= neededPartListPanels; i++) {
                Transform t = Instantiate(partListPanelPrefab);
                t.SetParent(this.transform);
                t.GetComponent<PartListPanel>().partList = this;
                t.gameObject.SetActive(false);
                partListPanels.Add(t.gameObject);
            }
        }
        int index = 0;
        foreach (Part p in parts) {
            partListPanels[index].SetActive(true);
            partListPanels[index].GetComponent<PartListPanel>().DisplayPart(p);
            index += 1;
        }
    }

    public void Clear() {
        for (int i = 0; i < partListPanels.Count; i++) {
            partListPanels[i].SetActive(false);
        }
    }
}
