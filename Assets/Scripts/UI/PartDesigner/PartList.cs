using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PartList : MonoBehaviour {
    public GameObject partDisplayPanelPrefab;
    private GameObjectPool partDisplayPanelPool;
    public PartDisplay selectedDisplay;
    public event Action<Part> OnPartSelectedEvent;

    private void Awake() {
        partDisplayPanelPool = new GameObjectPool(partDisplayPanelPrefab, gameObject, SubscribeToSelection);
    }

    public void SubscribeToSelection(GameObject g) {
        g.GetComponent<PartDisplay>().OnPartDisplaySelectedEvent += PartDisplaySelected;
    }

    public void PartDisplaySelected(PartDisplay display) {
        selectedDisplay?.DeselectPartDisplay();
        selectedDisplay = display;
        OnPartSelectedEvent?.Invoke(selectedDisplay.part);
    }

    public void DisplayParts(Part[] parts) {
        Clear();
        foreach( Part p in parts) {
            partDisplayPanelPool.GetGameObject().GetComponent<PartDisplay>().DisplayPart(p);
        }
    }

    public void Clear() {
        partDisplayPanelPool.ReleaseAll();
    }
}
