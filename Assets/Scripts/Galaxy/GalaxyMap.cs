﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameConstructs;

public class GalaxyMap : MonoBehaviour, IPointerClickHandler {
    public GameObjectPool sectorPool;
    public GameObject sectorPrefab;
    public GameObject galaxyMapRoot;
    public GalaxyEntityPanel galaxyEntityPanel;
    public Sector[][] sectors;
    public List<Sector> allSectors = new List<Sector>();
    public GridLayoutGroup layoutGroup;
    private Sector hoverSector;
    public Sector HoverSector {
        get { return hoverSector; }
        set {
            hoverSector = value;
            SectorDisplayUpdate();            
        }
    }
    private Sector selectedSector;
    public Sector SelectedSector {        
        get { return selectedSector; }
        set {
            selectedSector = value;
            SectorDisplayUpdate();
        }
    }

    public int width = 32;
    public int height = 32;
    public int buldgeCount = 3000;
    public int armCount = 3000;
    public int numberOfArms = 4;
    public float hubRadius = 15.0f; //radius of buldge
    public float diskRadius = 25.0f; //radius of disk
    public float armRadius = 45.0f; //radius of arms
    public float armWinding = 0.5f; // how tightly the spirals wind
    public float armWidth = 30.0f; // in degrees
    public float fuzzFactor = 10.0f;

    public Gradient gradient;
    public GradientColorKey[] colorKeys = new GradientColorKey[4] {
        new GradientColorKey(Color.black, 0.0f),
        new GradientColorKey(Color.red, 0.2f),
        new GradientColorKey(Color.yellow, 0.4f),
        new GradientColorKey(Color.white, 1.0f)
    };
    public GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2] {
        new GradientAlphaKey(1.0f, 0.0f),
        new GradientAlphaKey(1.0f,1.0f)
    };

    public Text galaxyTileDescription;

    public void SetGalaxyTile(GameObject g) {
        g.GetComponent<Sector>().map = this;
    }

    public void ShowTerritory() {
        foreach(Sector s in allSectors) {
            s.ShowOwnerColor();
        }
    }

    public void ShowSystems() {
        foreach (Sector s in allSectors) {
            s.ShowBaseColor();
        }
    }

    public void ShowNoFeatures() {
        ShowFeatures(GalaxyFeatureType.None);
    }

    public void ShowCapitalFeatures() {
        ShowFeatures(GalaxyFeatureType.EntityCapital);
    }

    private void ShowFeatures(GalaxyFeatureType t) {
        foreach(Sector s in allSectors) {
            s.ShowFeatures(t);
        }
    }
    
    public void Initialize() {
        sectorPool = new GameObjectPool(sectorPrefab, galaxyMapRoot, SetGalaxyTile);
        sectors = new Sector[width][];
        for(int i = 0; i < width; i++) {
            sectors[i] = new Sector[height];
            for(int j = 0; j < height; j++) {
                GameObject g = sectorPool.GetGameObject();
                Sector s = g.GetComponent<Sector>();
                s.sectorName = "Sector " + (j + i * width).ToString();
                s.coord = new Coord(i, j);
                sectors[i][j] = s;
                allSectors.Add(s);
            }
        }
        //precompute Neighbours
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                for (int k = 0; k < 8; k++) {
                    int neighbourX = i + Sector.neighbourDeltas[k].x;
                    int neighbourY = j + Sector.neighbourDeltas[k].y;
                    if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
                        sectors[i][j].neighbours[k] = sectors[neighbourX][neighbourY];
                    } else {
                        sectors[i][j].neighbours[k] = null;
                    }
                }
            }
        }
        gradient.SetKeys(colorKeys, alphaKeys);
        GenerateGalaxy();        
    }

    public void GenerateGalaxy() {
        ResetTiles();
        int maxCount = 0; //used for normalizing

        for(int i= 0; i < buldgeCount + armCount; i++) {
            Coord c;
            if( i > buldgeCount) {
                c = GetStar(false);
            } else {
                c = GetStar(true);
            }
            if(c.x < width && c.y < height && c.x >= 0 && c.y >= 0) {
                sectors[c.x][c.y].systemCount += 1;
                if (sectors[c.x][c.y].systemCount > maxCount) {
                    maxCount = sectors[c.x][c.y].systemCount;
                }
            }
        }

        int x = 0;
        int y = 0;

        for(int i = 0; i < width*height; i++) {
            sectors[x][y].baseColor = gradient.Evaluate(sectors[x][y].systemCount / (float) maxCount);
            sectors[x][y].ShowBaseColor();
            x += 1;
            if (x >= width) {
                x = 0;
                y += 1;
            }
        }
    }

    public void TilePointerEnter(Sector tile) {
        HoverSector = tile;
        tile.SetHover(true);
        string displayString = "Major Systems: " + tile.systemCount.ToString();
        if (tile.Owner != null) {
            displayString += "\nOwner: " + tile.Owner.entityName;
        } else {
            displayString += "\nUnclaimed";
        }
        foreach(GalaxyFeature f in tile.features) {
            switch (f.featureType) {
                case GalaxyFeatureType.EntityCapital:
                    displayString += "\nCapital System of " + tile.Owner.entityName;
                    break;
                default:
                    Debug.LogError("Unsupported feature type on tile pointer enter");
                    break;
            }
        }
        galaxyTileDescription.text = displayString;
    }

    public void TilePointerExit(Sector tile) {
        tile.SetHover(false);
        if(HoverSector == tile) {
            HoverSector = null;
        }
    }

    public void OnPointerClick(PointerEventData data) {
        if(HoverSector != null) {
            if (SelectedSector != null) {
                SelectedSector.SetSelection(false);
            }
            if (SelectedSector != HoverSector) {
                SelectedSector = HoverSector;
                SelectedSector.SetSelection(true);
            }
        } 
    }

    private void SectorDisplayUpdate() {
        if(selectedSector != null && selectedSector.Owner != null) {
            galaxyEntityPanel.gameObject.SetActive(true);
            galaxyEntityPanel.DisplayEntity(selectedSector.Owner);
        } else if (hoverSector != null && hoverSector.Owner != null) {
            galaxyEntityPanel.gameObject.SetActive(true);
            galaxyEntityPanel.DisplayEntity(hoverSector.Owner);
        } else {
            galaxyEntityPanel.gameObject.SetActive(false);
        }
    }

    private Coord GetStar(bool buldge) {
        if (buldge) {
            float distance = Random.Range(0.0f, hubRadius);
            float distanceFuzz = distance + Random.Range(-hubRadius/ fuzzFactor, hubRadius / fuzzFactor);

            float theta = Random.Range(0.0f, 360.0f);
            int x = width / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            int y = height / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            return new Coord(x, y);
        } else {
            //arms
            float armDelta = 0.0f;
            if (numberOfArms != 0) {
                armDelta = 360 / numberOfArms;
            }
            float distance = hubRadius + Random.Range(0.0f, diskRadius);
            float distanceFuzz = distance + Random.Range(-diskRadius/ fuzzFactor, diskRadius / fuzzFactor);
            float theta = ((360.0f * armWinding * (distanceFuzz / diskRadius)));
            theta += Random.Range(0.0f, armWidth);
            theta += armDelta * (float)Random.Range(0, numberOfArms);
            theta += Random.Range(0.0f, fuzzFactor);
            int x = width / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            int y = height / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            return new Coord(x, y);
        }

    }

    private void ResetTiles() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                sectors[i][j].Clear();
            }
        }
    }
}