using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ResearchNode {
    public int x;
    public int y;
    public int cost = 100;
    public int progress = 0;
    public bool active = false;
    public bool complete = false;
    public bool locked = false;
    public bool mandatory = false;
    public Action<ResearchNode> onComplete;
    public Action onUpdate;
    public Vector2 key; //unlocks any tech at this location
    public Color nodeColor;
    public string name;
    public string effect;

    public ResearchNode(int _cost, string _name, string _effect, bool _mandatory) {
        cost = _cost;
        name = _name;
        effect = _effect;
        mandatory = _mandatory;
    }

    public void UpdateResearch(int _progress) {
        progress += _progress;
        onUpdate?.Invoke();
        if (progress > cost) {
            onComplete(this);
        }
    }
}
