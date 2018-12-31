using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameObjectPool {
    private List<GameObject> pool = new List<GameObject>();
    public List<GameObject> outOfPool = new List<GameObject>();
    private GameObject prefab;
    private GameObject attachPoint;
    private Action<GameObject> setupAction;

    public GameObject GetGameObject() {
        if(pool.Count < 1) {
            GameObject g = GameObject.Instantiate(prefab);
            if(setupAction != null) {
                setupAction(g);
            }
            g.transform.SetParent(attachPoint.transform);
            outOfPool.Add(g);
            return g;
        } else {
            GameObject g = pool[0];
            outOfPool.Add(g);
            pool.Remove(g);
            g.SetActive(true);
            return g;
        }
    }

    public void ReleaseGameObject(GameObject released) {
        pool.Add(released);
        outOfPool.Remove(released);
        released.SetActive(false);
    }

    public void ReleaseAll() {
        foreach(GameObject g in outOfPool) {
            pool.Add(g);
        }
        outOfPool.Clear();
    }

    public GameObjectPool(GameObject _prefab, GameObject _attachpoint, Action<GameObject> _setupAction=null) {
        prefab = _prefab;
        attachPoint = _attachpoint;
        setupAction = _setupAction;
    }
}
