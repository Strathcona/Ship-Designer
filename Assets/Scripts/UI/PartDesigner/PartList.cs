using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartList : MonoBehaviour {
    public GameObject partListPanelPrefab;
    private GameObjectPool partListPanelPool;
    public PartDesigner partDesigner;

    private void Awake() {
        partListPanelPool = new GameObjectPool(partListPanelPrefab, this.gameObject, SetupAction);
    }

    public void EditPart(Part p) {
        partDesigner.LoadPart(p);
    }

    public void SetupAction(GameObject g) {
        g.GetComponent<PartListPanel>().partList = this;
    }

    public void DisplayParts() {
        Clear();
        Part[] parts = DesignManager.instance.GetAllParts();
        int index = 0;
        foreach (Part p in parts) {
            GameObject g = partListPanelPool.GetGameObject();
            g.GetComponent<PartListPanel>().DisplayPart(p);
            index += 1;
        }
    }

    public void Clear() {
        partListPanelPool.ReleaseAll();
    }
}
