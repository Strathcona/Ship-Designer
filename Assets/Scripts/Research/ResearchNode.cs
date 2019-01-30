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
    public int progress = 0;
    public bool active = false;
    public bool complete = false;
    public ResearchNodeType nodeType;
    public Color nodeColor;
    public string name;
    public string effect;

    public ResearchNode(int _cost, string _name, string _effect, ResearchNodeType _nodeType) {
        cost = _cost;
        name = _name;
        effect = _effect;
        nodeType = _nodeType;
    }
}
