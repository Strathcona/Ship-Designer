using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameObjectPool {
    public List<GameObject> freeObjects = new List<GameObject>();
    public List<GameObject> usedObjects = new List<GameObject>();
    public List<MonoBehaviour> usedScripts = new List<MonoBehaviour>();
    private GameObject prefab;
    private GameObject attachPoint;
    private Action<GameObject> onCreation;

    public GameObject GetGameObject() {
        if(freeObjects.Count < 1) {
            GameObject g = GameObject.Instantiate(prefab, attachPoint.transform) as GameObject;
            g.name = prefab.name + attachPoint.transform.childCount.ToString();
            onCreation?.Invoke(g);
            usedObjects.Add(g);
            return g;
        } else {
            GameObject g = freeObjects[0];
            usedObjects.Add(g);
            freeObjects.Remove(g);
            g.SetActive(true);
            g.transform.SetAsLastSibling();
            return g;
        }
    }

    public List<T> GetComponentOfUsedObjects<T>() {
        List<T> t = new List<T>();
        foreach(GameObject g in usedObjects) {
            t.Add(g.GetComponent<T>());
        }
        return t;
    }

    public void ReleaseGameObject(GameObject released) {
        freeObjects.Add(released);
        usedObjects.Remove(released);
        released.SetActive(false);
    }

    public void ReleaseAll() {
        foreach(GameObject g in usedObjects) {
            freeObjects.Add(g);
            g.SetActive(false);
        }
        usedObjects.Clear();
    }

    public GameObjectPool(GameObject _prefab, GameObject _attachpoint, Action<GameObject> _onCreation=null) {
        prefab = _prefab;
        attachPoint = _attachpoint;
        onCreation = _onCreation;
    }
}
