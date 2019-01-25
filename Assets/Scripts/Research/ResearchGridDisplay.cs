using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ResearchGridDisplay : MonoBehaviour {
    public GameObject GridRoot;
    public int xSize;
    public int ySize;
    public ResearchNode[][] nodes;
    ResearchNode startNode;
    public List<ResearchNode> finishedNodes = new List<ResearchNode>();
    public List<ResearchNode> isolatedNodes = new List<ResearchNode>();
    public List<ResearchNode> activeNodes = new List<ResearchNode>();
    public List<ResearchNode> recentlyFinishedNodes = new List<ResearchNode>();

    public int researchPerMinute = 1;

    private void Start() {
        TimeManager.instance.actionsOnMinute.Add(ResearchUpdate);
        nodes = new ResearchNode[xSize][];
        for(int i = 0; i < xSize; i++) {
            nodes[i] = new ResearchNode[ySize];
        }
        InitializeGrid();
        DisplayGrid();
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
            List<ResearchNode> next = GetAdjacent(node.x, node.y);
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
        Debug.Log("Loading Research Grids");
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Research");
        FileInfo[] files = d.GetFiles("*.csv");
        foreach(FileInfo file in files) {
            string[] lines = File.ReadAllLines(file.FullName);
            Dictionary<int, ResearchNode> locks = new Dictionary<int, ResearchNode>();
            Dictionary<int, ResearchNode> keys = new Dictionary<int, ResearchNode>();

            for(int y = 0; y < ySize; y++) {
                int x = 0;
                foreach (string s in lines[y].Split(',')) {
                    switch (s[0]) {
                        case 'R':
                            nodes[x][y] = new ResearchNode(x, y, Color.white, FinishedResearch);
                            nodes[x][y].name = "Random Tech";
                            break;
                        case 'S':
                            nodes[x][y] = new ResearchNode(x, y, Color.green, FinishedResearch);
                            nodes[x][y].name = "Start Tech";
                            startNode = nodes[x][y];
                            break;
                        case 'E':
                            nodes[x][y] = new ResearchNode(x, y, Color.red, FinishedResearch);
                            nodes[x][y].name = "End Tech";
                            break;
                        case 'K':
                            nodes[x][y] = new ResearchNode(x, y, Color.grey, FinishedResearch);
                            nodes[x][y].name = "Key Tech";
                            int keyID;
                            int.TryParse(s.Substring(1,1), out keyID);
                            keys.Add(keyID, nodes[x][y]);
                            break;
                        case 'L':
                            nodes[x][y] = new ResearchNode(x, y, Color.grey, FinishedResearch);
                            nodes[x][y].name = "Locked Tech";
                            nodes[x][y].locked = true;
                            int lockID;
                            int.TryParse(s.Substring(1, 1), out lockID);
                            locks.Add(lockID, nodes[x][y]);
                            break;
                        default:
                            Debug.Log("Bad character parse in reading research grid in file " + file.Name + " in line " + y);
                            break;
                    }
                    x += 1;
                }
            }


        }
    }

    private void DisplayGrid() {
        int xindex = 0;
        int yindex = 0;
        for(int i = 0; i < GridRoot.transform.childCount; i++) {
            ResearchNodePanel panel = GridRoot.transform.GetChild(i).GetComponent<ResearchNodePanel>();
            panel.DisplayResearchNode(nodes[xindex][yindex]);
            nodes[xindex][yindex].onUpdate = panel.Refresh;
            xindex += 1;
            if(xindex >= xSize) {
                xindex = 0;
                yindex += 1;
            }
        }
    }

    private List<ResearchNode> GetAdjacent(int x, int y) {
        List<ResearchNode> nodesToReturn = new List<ResearchNode>();
        if( x + 1 < xSize) {//not too far right
            ResearchNode node = nodes[x + 1][y];
            if(!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (x -1 >= 0) {//not too far left
            ResearchNode node = nodes[x -1][y];
            if (!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (y + 1 < ySize) {//not too far down
            ResearchNode node = nodes[x][y + 1];
            if (!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        if (y - 1 >= 0) {//not too far up
            ResearchNode node = nodes[x][y - 1];
            if (!node.complete && !node.locked && !node.active) {
                nodesToReturn.Add(node);
            }
        }
        return nodesToReturn;
    }

}
