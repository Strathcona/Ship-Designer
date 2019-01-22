using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartIconLayout : MonoBehaviour {
    public GameObjectPool partIconPool;
    public GameObject partIconPrefab;
    public GameObject partIconRoot;

    private void Awake() {
        partIconPool = new GameObjectPool(partIconPrefab, partIconRoot);
    }

    public void DisplayParts(Dictionary<Part, int> parts) {
        Clear();
        foreach (Part p in parts.Keys) {
            GameObject g = partIconPool.GetGameObject();
            PartIcon pi = g.GetComponent<PartIcon>();
            pi.DisplayPart(p, parts[p]);
        }
    }

    public void Clear() {
        partIconPool.ReleaseAll();
    }
}
