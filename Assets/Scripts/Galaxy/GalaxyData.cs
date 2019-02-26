using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyData {
    public SectorData[][] sectors;
    public List<GalaxyEntity> entities = new List<GalaxyEntity>();
    public int maxCount;

    public GalaxyData(int size) {
        sectors = new SectorData[size][];
        for(int i = 0; i < size; i++) {
            sectors[i] = new SectorData[size];
            for(int j =0; j < size; j++) {
                SectorData s = new SectorData();
                s.sectorName = "Sector " + (j + i * size).ToString();
                s.coord = new Coord(i, j);
                sectors[i][j] = s;
            }
        }

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                for (int k = 0; k < 8; k++) {
                    int neighbourX = i + SectorData.neighbourDeltas[k].x;
                    int neighbourY = j + SectorData.neighbourDeltas[k].y;
                    if (neighbourX >= 0 && neighbourX < size && neighbourY >= 0 && neighbourY < size) {
                        sectors[i][j].neighbours[k] = sectors[neighbourX][neighbourY];
                    } else {
                        sectors[i][j].neighbours[k] = null;
                    }
                }
            }
        }
    }

    public void ClearEntityData() {
        for(int i = 0; i < sectors.Length; i++) {
            for(int j =0; j < sectors[0].Length; j++) {
                sectors[i][j].RemoveOwner();
            }
        }
        entities.Clear();
    }
}
