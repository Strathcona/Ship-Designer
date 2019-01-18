using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHullsizeDisplay : MonoBehaviour {

    public GameObject hullSizePrefab;
    public GameObject hullSizeRoot;
    public List<GameObject> hullsizeBoxes = new List<GameObject>();
    public Ship ship;

    public void DisplayShip(Ship s) {
        Clear();
        ship = s;
        int totalBoxes = 0;
        foreach(Part p in ship.parts.Keys) {
            totalBoxes += p.Size * ship.parts[p];
        }
        totalBoxes += ship.lifeSupportSize;
        int neededBoxes = totalBoxes - hullsizeBoxes.Count;
        for(int i = 0; i < neededBoxes; i++) {
            GameObject g = Instantiate(hullSizePrefab) as GameObject;
            g.transform.SetParent(hullSizeRoot.transform);
            g.SetActive(false);
            hullsizeBoxes.Add(g);
        }

        int index = 0;
        foreach (Part p in ship.parts.Keys) {
            for (int j = 0; j < ship.parts[p]; j++) {
                for (int i = 0; i < p.Size; i++) {
                    hullsizeBoxes[index].SetActive(true);
                    hullsizeBoxes[index].GetComponent<HullsizeBox>().SetPart(p);
                    index += 1;
                }
            }
        }
        for(int i = 0; i < ship.lifeSupportSize; i++) {
            hullsizeBoxes[index].SetActive(true);
            hullsizeBoxes[index].GetComponent<HullsizeBox>().SetLifeSupport();
            index += 1;
        }
    }

    public void Clear() {
        foreach(GameObject g in hullsizeBoxes) {
            g.GetComponent<HullsizeBox>().Clear();
            g.SetActive(false);
        }
    }
}
