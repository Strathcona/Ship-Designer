using UnityEngine;
using System.Collections;

public class ResearchGrid {
    public string[][] grid;
    public Coord start;
    public Coord end;
    public int xSize;
    public int ySize;

    public ResearchGrid(string[][] _grid, Coord _start, Coord _end) {
        grid = _grid;
        start = _start;
        end = _end;
        xSize = grid.Length;
        ySize = grid[0].Length;
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
        start = new Coord(start.x, ySize - start.y);
        end = new Coord(end.x, ySize - end.y);
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
        start = new Coord(xSize -start.x, start.y);
        end = new Coord(xSize - end.x, end.y);
        grid = newGrid;
    }
}
