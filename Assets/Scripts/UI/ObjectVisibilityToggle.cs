using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectVisibilityToggle : MonoBehaviour
{
    public List<GameObject> objectsToToggle = new List<GameObject>();
    public GameObject currentObject;
    public string autoAddIfContains = "";
    public List<Action> onChange = new List<Action>();

    private void Start() {
        for(int i =0; i < transform.childCount; i++) {
            if(transform.GetChild(i).name == autoAddIfContains) {
                objectsToToggle.Add(transform.GetChild(i).gameObject);
            }
        }
        if(currentObject != null) {
            DisplayObject(currentObject.name);
        }
    }

    public string GetCurrentObjectName() {
        return currentObject.name;
    }

    public void DisplayObject(string objectName) {
        DisableAllObjects();
        GameObject target = objectsToToggle.Find(i => i.name == objectName);
        if (target != null) {
            target.SetActive(true);
            currentObject = target;
        } else {
            Debug.LogError("Object Visibility Toggle Couldn't Find Object " + objectName);
        }
        foreach (Action a in onChange) {
            a();
        }
    }

    private void DisableAllObjects() {
        foreach (GameObject g in objectsToToggle) {
            g.gameObject.SetActive(false);
        }
    }
}
