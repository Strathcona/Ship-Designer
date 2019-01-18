using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartDisplay : MonoBehaviour {
    public Ship ship;
    public Transform partDisplayPrefab;
    public List<GameObject> partDisplays = new List<GameObject>();

    public void DisplayShip(Ship s) {
        Clear();

        if(s.parts.Keys.Count > partDisplays.Count) {
            int neededPartDisplays = s.parts.Keys.Count - partDisplays.Count;
            Debug.Log("Needed partDisplays " + neededPartDisplays);
            for(int i = 0; i <= neededPartDisplays; i++) {
                Transform t = Instantiate(partDisplayPrefab);
                t.SetParent(this.transform);
                t.gameObject.SetActive(false);
                partDisplays.Add(t.gameObject);
            }
        }
        int index = 0;
        foreach (Part p in s.parts.Keys) {
            partDisplays[index].SetActive(true);
            partDisplays[index].GetComponent<PartDisplay>().DisplayPart(p);
            index += 1;
        }
    }

    public void Clear() {
        ship = null;
        for (int i = 0; i < partDisplays.Count; i++) {
            partDisplays[i].SetActive(false);
        }
    }
}
