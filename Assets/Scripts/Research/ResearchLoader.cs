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
            List<ResearchGrid> gridList = new List<ResearchGrid>();
            string[] lines = File.ReadAllLines(file.FullName);
            int currentLine = 0;
            string[][] grid = GetSquareGrid(Constants.researchGridSize);
            Coord start = new Coord(0, 0);
            Coord end = new Coord(0, 0);
            foreach(string line in lines) {
                string[] split = line.Split(',');
                for(int i = 0; i < line.Length; i ++ ){
                    if(split[i][0] == 'S') {
                        start = new Coord(currentLine, i);
                    } else if(split[i][0] == 'E') {
                        end = new Coord(currentLine, i);
                    }
                }
                currentLine += 1;
                if(currentLine > Constants.researchGridSize) {
                    ResearchGrid researchGrid = new ResearchGrid(grid, start, end);
                    gridList.Add(researchGrid);
                    grid = GetSquareGrid(Constants.researchGridSize);
                    currentLine = 0;
                }
            }
            researchGrids.Add(file.Name.Substring(0, file.Name.Length - 3), gridList);
        }

        Debug.Log("Loading Research Nodes");
        FileInfo[] txtFiles = d.GetFiles("*.txt");
        foreach (FileInfo file in txtFiles) {
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
                    } else {
                        Debug.LogError("Bad int parsed in " + file.FullName + " line " + i);
                    }

                }
            }
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
        string[][] grid = new string[Constants.researchGridSize][];
        for (int i = 0; i < Constants.researchGridSize) {
            grid[i] = new string[Constants.researchGridSize];
        }
        return grid;
    }
}
