using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using GameConstructs;

public class ResearchGridDisplay : MonoBehaviour {
    public GameObject GridRoot;
    public GameObjectPool ResearchPanelPool;
    public GameObject ResearchPanelPrefab;
    public int xSize;
    public int ySize;
    public int currentTier = 0;
    public ResearchNode[][][] nodes; // [tier][x][y]
    public ResearchNodePanel[][][] panels;
    ResearchNode startNode;
    public List<ResearchNode> finishedNodes = new List<ResearchNode>();
    public List<ResearchNode> isolatedNodes = new List<ResearchNode>();
    public List<ResearchNode> activeNodes = new List<ResearchNode>();
    public List<ResearchNode> recentlyFinishedNodes = new List<ResearchNode>();

    public int researchPerUpdate = 1;

    private void Start() {
        TimeManager.instance.actionsOnMinute.Add(ResearchUpdate);
        ResearchPanelPool = new GameObjectPool(ResearchPanelPrefab, GridRoot);
        //nodes = new ResearchNode[Constants.numberOfTiers][][];
        nodes = new ResearchNode[1][][];
        for (int i = 0; i <nodes.Length; i++) {
            nodes[i] = new ResearchNode[xSize][];
            for(int j = 0; j < nodes[i].Length; j++) {
                nodes[i][j] = new ResearchNode[ySize];
            }
        }
        InitializeGrid();
        DisplayGrid(0);
        recentlyFinishedNodes.Add(startNode);
    }

    public void ResearchUpdate() {
        foreach(ResearchNode r in activeNodes) {
            r.progress += researchPerUpdate;
            if(r.progress > r.cost) {
                recentlyFinishedNodes.Add(r);
            }
            panels[r.tier][r.x][r.y].Refresh();
        }
        foreach(ResearchNode node in recentlyFinishedNodes) {
            activeNodes.Remove(node);
            node.active = false;
            node.complete = true;
            panels[node.tier][node.x][node.y].Refresh();
            finishedNodes.Add(node);
            List<ResearchNode> next = GetAdjacent(node.tier, node.x, node.y);
            foreach (ResearchNode n in next) {
                n.active = true;
                activeNodes.Add(n);
                panels[n.tier][n.x][n.y].Refresh();
            }
        }

    }

    private void InitializeGrid() {
        ResearchLoader.GetGrids("Tier0Layouts", out List<ResearchGrid> grids);
        ResearchGrid grid = grids[Random.Range(0, grids.Count)];
        ResearchLoader.GetNodes("Tier0Technologies", out List<ResearchNode> researchNodes);
        nodes = new ResearchNode[Constants.numberOfTiers][][];
        if(grid.FillGrid(researchNodes, 0, out ResearchNode[][] researchNodeGrid)) {
            nodes[0] = researchNodeGrid;
            DisplayGrid(0);

        } else {
            Debug.LogError("Could not intitialize Grid");
        }
        panels = new ResearchNodePanel[1][][];
        panels[0] = new ResearchNodePanel[nodes[0].Length][];
        for(int x = 0; x < nodes[0].Length; x++) {
            panels[0][x] = new ResearchNodePanel[nodes[0][x].Length];
            for(int y = 0; y < nodes[0][x].Length; y++) {
                GameObject g = ResearchPanelPool.GetGameObject();
                ResearchNodePanel panel = g.GetComponent<ResearchNodePanel>();
                panels[0][x][y] = panel;
            }
        }
    }

    private void DisplayGrid(int tier) {
        int xindex = 0;
        int yindex = 0;
        for(int i = 0; i < GridRoot.transform.childCount; i++) {
            ResearchNodePanel panel = GridRoot.transform.GetChild(i).GetComponent<ResearchNodePanel>();
            panel.DisplayResearchNode(nodes[tier][xindex][yindex]);
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
            if(!node.complete && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (x -1 >= 0) {//not too far left
            ResearchNode node = nodes[tier][x -1][y];
            if (!node.complete && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (y + 1 < ySize) {//not too far down
            ResearchNode node = nodes[tier][x][y + 1];
            if (!node.complete && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (y - 1 >= 0) {//not too far up
            ResearchNode node = nodes[tier][x][y - 1];
            if (!node.complete && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        return nodesToReturn;
    }
}
