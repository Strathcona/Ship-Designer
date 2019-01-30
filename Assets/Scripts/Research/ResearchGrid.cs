using UnityEngine;
using System.Collections.Generic;
using GameConstructs;

public class ResearchGrid {
    public string[][] grid;
    public int xSize;
    public int ySize;

    public ResearchGrid(string[][] _grid) {
        grid = _grid;
        xSize = grid.Length;
        ySize = grid[0].Length;
    }


    public bool FillGrid(List<ResearchNode> nodes, int tier, out ResearchNode[][] filledGrid) {
        List<ResearchNode> startNodes = nodes.FindAll(i => i.nodeType == ResearchNodeType.Start);
        List<ResearchNode> endNodes = nodes.FindAll(i => i.nodeType == ResearchNodeType.End);
        List<ResearchNode> mandatoryNodes = nodes.FindAll( i => i.nodeType == ResearchNodeType.Mandatory);
        List<ResearchNode> optionalNodes = nodes.FindAll(i => i.nodeType == ResearchNodeType.Optional);
        filledGrid = new ResearchNode[xSize][];
        for (int i = 0; i < xSize; i++) {
            filledGrid[i] = new ResearchNode[ySize];
        }
        List<Coord> coords = new List<Coord>(); //make a random list of coords 
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                coords.Insert(Random.Range(0, coords.Count), new Coord(x, y));
            }
        }
        Debug.Log(nodes.Count +" "+ grid.Length*grid[0].Length);
        int count = 0;
        foreach(Coord c in coords) {
            Debug.Log(grid[c.x][c.y] + " " + count);
            count += 1;
            switch (grid[c.x][c.y]) {
                case "R":
                    if(mandatoryNodes.Count > 1) {
                        filledGrid[c.x][c.y] = mandatoryNodes[0];
                        mandatoryNodes.RemoveAt(0);
                    } else if (optionalNodes.Count > 0) {
                        filledGrid[c.x][c.y] = optionalNodes[0];
                        optionalNodes.RemoveAt(0);
                    } else {
                        Debug.LogError("Not enough nodes to fill grid after " + count);
                        return false;
                    }
                    break;
                case "S":
                    if(startNodes.Count > 0) {
                        filledGrid[c.x][c.y] = startNodes[0];
                        startNodes.RemoveAt(0);
                    } else {
                        Debug.LogError("Not enough start nodes to fill grid");
                        return false;
                    }
                    break;
                case "E":
                    if (endNodes.Count > 0) {
                        filledGrid[c.x][c.y] = endNodes[0];
                        endNodes.RemoveAt(0);
                    } else {
                        Debug.LogError("Not enough end nodes to fill grid");
                        return false;
                    }
                    break;
                default:
                    Debug.LogError("Can't parse grid " + grid[c.x][c.y] + " in research grid");
                    break;
            }
        }
        return true;
    }

    public void MirrorHorizontal() {
        string[][] newGrid = new string[xSize][];
        for(int i = 0; i < xSize; i++) {
            newGrid[i] = new string[ySize];
        }

        for(int i = 0; i < xSize; i++) {
            for(int j = 0; j < ySize; j++) {
                newGrid[i][ySize - j] = grid[i][j];
            }
        }
        grid = newGrid;
    }

    public void MirrorVertical() {
        string[][] newGrid = new string[xSize][];
        for (int i = 0; i < xSize; i++) {
            newGrid[i] = new string[ySize];
        }

        for (int i = 0; i < xSize; i++) {
            for (int j = 0; j < ySize; j++) {
                newGrid[xSize - i][j] = grid[i][j];
            }
        }
        grid = newGrid;
    }
}
