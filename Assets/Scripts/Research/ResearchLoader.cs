using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameConstructs;

public static class ResearchLoader {

    public static Dictionary<string, List<ResearchGrid>> researchGrids = new Dictionary<string, List<ResearchGrid>>();
    public static Dictionary<string, List<ResearchNode>> researchNodes = new Dictionary<string, List<ResearchNode>>();

    static ResearchLoader() {
        Debug.Log("Loading Research Grids");
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Research");
        FileInfo[] csvFiles = d.GetFiles("*.csv");
        foreach (FileInfo file in csvFiles) {
            Debug.Log("Loading " + file.Name);
            List<ResearchGrid> gridList = new List<ResearchGrid>();
            string[] lines = File.ReadAllLines(file.FullName);
            string[] row;
            string[][] grid;
            List<string[]> rows = new List<string[]>();
            Coord start = new Coord(0, 0);
            Coord end = new Coord(0, 0);
            for(int i = 0; i < lines.Length; i++) {
                row = lines[i].Split(',');
                if(row.Length > 1) {
                    rows.Add(row);
                } else if (rows.Count >= 1){//if we have something to add
                    Debug.Log("Adding Grid "+rows.Count+" "+rows[0].Length);
                    grid = new string[rows[0].Length][];
                    for(int j = 0; j < rows.Count; j++) {
                        grid[j] = rows[j];
                    }
                    ResearchGrid researchGrid = new ResearchGrid(grid);
                    gridList.Add(researchGrid);
                    rows.Clear();
                }
            }
            //just to check at the end if we have any left over grids
            if(rows.Count >= 1) {
                Debug.Log("Adding Grid " + rows.Count + " " + rows[0].Length);
                grid = new string[rows[0].Length][];
                for (int j = 0; j < rows.Count; j++) {
                    grid[j] = rows[j];
                }
                ResearchGrid researchGrid = new ResearchGrid(grid);
                gridList.Add(researchGrid);
                rows.Clear();
            }
            researchGrids.Add(file.Name.Substring(0, file.Name.Length - 4), gridList);
        }

        Debug.Log("Loading Research Nodes");
        FileInfo[] txtFiles = d.GetFiles("*.txt");
        foreach (FileInfo file in txtFiles) {
            Debug.Log("Loading "+file.Name);
            List<ResearchNode> nodeList = new List<ResearchNode>();
            string[] lines = File.ReadAllLines(file.FullName);
            for(int i = 0; i < lines.Length; i++) {
                List<string> strings = Utility.SplitOnTopLevelBrackets(lines[i]);
                if(strings.Count < 4) {
                    Debug.LogError("Bad string parsed in "+file.FullName+ " line "+i);
                } else {
                    ResearchNodeType nodeType;
                    if(strings[0] == "MANDATORY") {
                        nodeType = ResearchNodeType.Mandatory;
                    }else if (strings[0] == "END") {
                        nodeType = ResearchNodeType.End;
                    }else if (strings[0] == "START") {
                        nodeType = ResearchNodeType.Start;
                    } else {
                        nodeType = ResearchNodeType.Optional;
                    }
                    if (int.TryParse(strings[1], out int cost)) {
                        string effect = strings[2];
                        string name = strings[3];
                        ResearchNode node = new ResearchNode(cost, name, effect, nodeType);
                        nodeList.Add(node);
                    } else {
                        Debug.LogError("Bad int parsed in " + file.FullName + " line " + i);
                    }

                }
            }
            researchNodes.Add(file.Name.Substring(0, file.Name.Length - 4), nodeList);
        }
    }

    public static bool GetGrids(string gridName, out List<ResearchGrid> grid) {
        if (researchGrids.ContainsKey(gridName)) {
            grid = researchGrids[gridName];
            return true;
        }else {
            Debug.LogError("Couldn't find Grid Name "+gridName);
            grid = null;
            return false;
        }
    }

    public static bool GetNodes(string nodeName, out List<ResearchNode> nodes) {
        if (researchNodes.ContainsKey(nodeName)) {
            nodes = researchNodes[nodeName];
            return true;
        } else {
            Debug.LogError("Couldn't find Node Name " + nodeName);
            nodes = null;
            return false;
        }
    }

    public static string[][] GetSquareGrid(int size) {
        string[][] grid = new string[size][];
        for (int i = 0; i < size; i++) {
            grid[i] = new string[size];
        }
        return grid;
    }
}
