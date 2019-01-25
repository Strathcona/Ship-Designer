using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ResearchNode {
    public int x;
    public int y;
    public int tier = 0;
    public int cost = 100;
    public int progress = 0;
    public bool active = false;
    public bool complete = false;
    public Action<ResearchNode> onComplete;
    public Action onUpdate;
    public bool locked = false;
    public Vector2 key; //unlocks any tech at this location
    public Color nodeColor;
    public string name;
    public string effect;

    public ResearchNode(int _x, int _y, Color _color, Action<ResearchNode> _onComplete) {
        x = _x;
        y = _y;
        nodeColor = _color;
        onComplete = _onComplete;
    }

    public void UpdateResearch(int _progress) {
        progress += _progress;
        onUpdate?.Invoke();
        if (progress > cost) {
            onComplete(this);
        }
    }
}
