﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class TweakableEditor : MonoBehaviour {
    public GameObjectPool tweakablePanelPool;
    public GameObject tweakablePanelRoot;
    public GameObject tweakablePanelPrefab;

    private void Awake() {
        Debug.Log("Loading Tweakable Editor Panel Prefab");
        tweakablePanelPrefab = Resources.Load("Prefabs/Tweakable Editor Panel", typeof(GameObject)) as GameObject;
        tweakablePanelPool = new GameObjectPool(tweakablePanelPrefab, tweakablePanelRoot);
    }

    public void DisplayTweakables(Part p) {
        Clear();
        foreach (Tweakable t in p.tweakables) {
            GameObject g = tweakablePanelPool.GetGameObject();
            g.GetComponent<TweakableEditorPanel>().DisplayTweakable(t);
        }
    }

    public void Clear() {
        tweakablePanelPool.ReleaseAll();
    }


}
