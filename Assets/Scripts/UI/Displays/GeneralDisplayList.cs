using UnityEngine;
using System.Collections;
using System;

public class GeneralDisplayList: MonoBehaviour {

    public GameObjectPool displayPool;
    public GameObject entityDisplayPrefab;
    public GameObject root;
    public GeneralDisplay selected;
    public bool selectable = false;
    public event Action<IDisplayed> OnListElementSelectedEvent;

    public void Awake() {
        displayPool = new GameObjectPool(entityDisplayPrefab, root);
    }

    public void ForceSelect(int index) {
        if (selectable) {
            if(index < displayPool.usedObjects.Count) {
                GeneralDisplay d = displayPool.usedObjects[index].GetComponent<GeneralDisplay>();
                OnSelect(d);
            }
        }
    }

    public void Display(IDisplayed[] toDisplay) {
        displayPool.ReleaseAll();
        foreach (IDisplayed d in toDisplay) {
            GameObject g = displayPool.GetGameObject();
            GeneralDisplay display = g.GetComponent<GeneralDisplay>();
            display.Display(d);
            display.OnSelectEvent -= OnSelect;
            display.selectable = this.selectable;
            if (selectable) {
                display.OnSelectEvent += OnSelect;
            }
        }
    }

    public void OnSelect(GeneralDisplay d) {
        if(selected != null) {
            selected.Deselect();
        }
        OnListElementSelectedEvent(d.displayed);
        selected = d;
    }
}
