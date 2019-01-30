using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;
using System;

[System.Serializable]
public class ResearchNode {
    public int x;
    public int y;
    public int tier;
    public int cost = 100;
    private int progress = 0;
    public int Progress {
        get { return progress; }
        set {
            progress = value;
            onChange();
        }
    }
    private bool active = false;
    public bool Active {
        get { return active; }
        set {
            active = value;
            onChange();
        }
    }
    private bool complete = false;
    public bool Complete {
        get { return complete; }
        set {
            complete = value;
            onChange();
        }
    }
    public ResearchNodeType nodeType;
    public Color nodeColor;
    public string name;
    public string effect;
    public Action onChange;

    public ResearchNode(int _cost, string _name, string _effect, ResearchNodeType _nodeType) {
        cost = _cost;
        name = _name;
        effect = _effect;
        nodeType = _nodeType;
    }


}
