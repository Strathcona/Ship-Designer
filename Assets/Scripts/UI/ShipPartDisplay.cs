using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartDisplay : MonoBehaviour {
    public Ship ship;
    public Transform partDisplayPrefab;
    public List<GameObject> partDisplays = new List<GameObject>();

    public void DisplayShip(Ship s) {
        ClearDisplay();

        if(s.allParts.Count > partDisplays.Count) {
            int neededPartDisplays = s.allParts.Count - partDisplays.Count;
            Debug.Log("Needed partDisplays " + neededPartDisplays);
            for(int i = 0; i <= neededPartDisplays; i++) {
                Transform t = Instantiate(partDisplayPrefab);
                t.SetParent(this.transform);
                t.gameObject.SetActive(false);
                partDisplays.Add(t.gameObject);
            }
        }
        int index = 0;
        foreach (Part p in s.allParts) {
            partDisplays[index].SetActive(true);
            partDisplays[index].GetComponent<PartDisplay>().DisplayPart(p);
            index += 1;
        }
    }

    public void ClearDisplay() {
        ship = null;
        for (int i = 0; i < partDisplays.Count; i++) {
            partDisplays[i].SetActive(false);
        }
    }
}
