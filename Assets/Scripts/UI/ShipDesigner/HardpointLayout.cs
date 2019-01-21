using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardpointLayout : MonoBehaviour {

    public List<GameObject> hardpointDisplays = new List<GameObject>();
    public bool displayParts = false;
    public GameObject hardpointDisplayPrefab;

    public void DisplayHardpoints(List<Hardpoint> hardpoints) {
        Clear();
        int neededDisplays = hardpoints.Count - hardpointDisplays.Count;
        if(neededDisplays > 0) {
            for(int i = 0; i < neededDisplays; i++) {
                GameObject g = Instantiate(hardpointDisplayPrefab, transform) as GameObject;
                hardpointDisplays.Add(g);
            }
        }
        int index = 0;
        foreach(Hardpoint h in hardpoints) {
            hardpointDisplays[index].SetActive(true);
            hardpointDisplays[index].GetComponent<HardpointDisplay>().DisplayHardpoint(h);
            hardpointDisplays[index].GetComponent<HardpointDisplay>().displayPart = displayParts;
            index += 1;
        }
    }

    public void Clear() {
        foreach(GameObject g in hardpointDisplays) {
            g.GetComponent<HardpointDisplay>().Clear();
            g.SetActive(false);
        }
    }

}
