using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using GameConstructs;

public class ResearchGridDisplay : MonoBehaviour {
    public GameObject GridRoot;
    public int xSize;
    public int ySize;
    public int currentTier = 0;
    public ResearchNode[][][] nodes; // [tier][x][y]
    ResearchNode startNode;
    public List<ResearchNode> finishedNodes = new List<ResearchNode>();
    public List<ResearchNode> isolatedNodes = new List<ResearchNode>();
    public List<ResearchNode> activeNodes = new List<ResearchNode>();
    public List<ResearchNode> recentlyFinishedNodes = new List<ResearchNode>();

    public int researchPerMinute = 1;

    private void Start() {
        TimeManager.instance.actionsOnMinute.Add(ResearchUpdate);
        //nodes = new ResearchNode[Constants.numberOfTiers][][];
        nodes = new ResearchNode[0][][];
        for (int i = 0; i <nodes.Length; i++) {
            nodes[i] = new ResearchNode[xSize][];
            for(int j = 0; j < nodes[i].Length; j++) {
                nodes[i][j] = new ResearchNode[ySize];

            }
        }
        InitializeGrid();
        DisplayGrid(0);
        FinishedResearch(startNode);
    }

    public void ResearchUpdate() {
        foreach(ResearchNode r in activeNodes) {
            r.UpdateResearch(researchPerMinute);
        }
        foreach(ResearchNode node in recentlyFinishedNodes) {
            activeNodes.Remove(node);
            node.active = false;
            node.complete = true;
            finishedNodes.Add(node);
            List<ResearchNode> next = GetAdjacent(node.tier, node.x, node.y);
            foreach (ResearchNode n in next) {
                n.active = true;
                activeNodes.Add(n);
            }
        }

    }

    public void FinishedResearch(ResearchNode node) {
        recentlyFinishedNodes.Add(node);
        //store them in a temporary array because we can't modify the main collection halfway through a foreach
    }

    private void InitializeGrid() {
        
    }

    private void DisplayGrid(int tier) {
        int xindex = 0;
        int yindex = 0;
        for(int i = 0; i < GridRoot.transform.childCount; i++) {
            ResearchNodePanel panel = GridRoot.transform.GetChild(i).GetComponent<ResearchNodePanel>();
            panel.DisplayResearchNode(nodes[tier][xindex][yindex]);
            nodes[tier][xindex][yindex].onUpdate = panel.Refresh;
            xindex += 1;
            if(xindex >= xSize) {
                xindex = 0;
                yindex += 1;
            }
        }
    }

    private List<ResearchNode> GetAdjacent(int tier, int x, int y) {
        List<ResearchNode> nodesToReturn = new List<ResearchNode>();
        if( x + 1 < xSize) {//not too far right
            ResearchNode node = nodes[tier][x + 1][y];
            if(!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (x -1 >= 0) {//not too far left
            ResearchNode node = nodes[tier][x -1][y];
            if (!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (y + 1 < ySize) {//not too far down
            ResearchNode node = nodes[tier][x][y + 1];
            if (!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (y - 1 >= 0) {//not too far up
            ResearchNode node = nodes[tier][x][y - 1];
            if (!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        return nodesToReturn;
    }
}
