using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;
using UnityEngine.UI;

public class ShipPartSelector : MonoBehaviour {
    public Event onPartsSelected;
    public Ship ship;
    public Text buttonText;
    public GameObject partIconPrefab;
    public List<GameObject> partIcons = new List<GameObject>();

    public void DisplayShipParts(Ship s) {
        ship = s;
        foreach (GameObject g in partIcons) {
            g.SetActive(false);
        }
        int neededIcons = ship.parts.Count - partIcons.Count;
        for (int i = 0; i < neededIcons; i++) {
            GameObject g = Instantiate(partIconPrefab) as GameObject;
            g.transform.SetParent(transform.GetChild(0));
            g.SetActive(false);
            partIcons.Add(g);
        }
        int index = 0;
        foreach (Part p in ship.parts) {
            partIcons[index].GetComponent<PartIcon>().DisplayPart(p);
            partIcons[index].SetActive(true);
            index += 1;
        }
    }
}
