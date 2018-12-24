using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHullsizeDisplay : MonoBehaviour
{
    public Ship ship;
    public Transform hullSizePrefab;
    public List<GameObject> hullSizeBoxes = new List<GameObject>();

    public void DisplayShip(Ship s) {
        ClearDisplay();
        ship = s;
        //check if we have enough boxes
        if (s.hullSize > hullSizeBoxes.Count) { 
            int neededBoxes = s.hullSize - hullSizeBoxes.Count;
            Debug.Log("Needed Boxes " + neededBoxes);
            //make more boxes
            for (int i = 0; i <= neededBoxes; i++) {
                Transform t = Instantiate(hullSizePrefab);
                t.SetParent(this.transform);
                t.gameObject.SetActive(false);
                hullSizeBoxes.Add(t.gameObject);
            }
        }
        int index = 0;
        int displayedHullSize = 0;
        foreach(Part p in s.allParts) {
            for(int i = 0; i < p.Size; i++) {
                hullSizeBoxes[index].SetActive(true);
                hullSizeBoxes[index].GetComponent<HullsizeBox>().SetPart(p);
                index += 1;
            }
            displayedHullSize += p.Size;
        }
        //display the empty hullsize
        int remainingHullsize = ship.hullSize - displayedHullSize;
        Debug.Log("Remaining hullsize " + remainingHullsize);
        for(int j = 0; j <= remainingHullsize; j++) {
            hullSizeBoxes[index].SetActive(true);
            index += 1;
        }
    }

    public void ClearDisplay() {
        ship = null;
        for(int i = 0; i < transform.childCount; i++) {
            hullSizeBoxes[i].GetComponent<HullsizeBox>().ClearPart();
            hullSizeBoxes[i].SetActive(false);
        }
    }
}
