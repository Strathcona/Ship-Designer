using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardpointLayout : MonoBehaviour {

    public GameObjectPool hardpointDisplayPool;
    public bool displayParts = false;
    public GameObject hardpointDisplayPrefab;
    public GameObject hardpointDisplayRoot;

    private void Awake() {
        hardpointDisplayPool = new GameObjectPool(hardpointDisplayPrefab, hardpointDisplayRoot);
    }

    public void DisplayHardpoints(List<Hardpoint> hardpoints) {
        Clear();
        foreach(Hardpoint h in hardpoints) {
            GameObject g = hardpointDisplayPool.GetGameObject();
            g.GetComponent<HardpointDisplay>().displayPart = displayParts;
            g.GetComponent<HardpointDisplay>().DisplayHardpoint(h);
        }
    }

    public void Clear() {
        hardpointDisplayPool.ReleaseAll();
    }
}
