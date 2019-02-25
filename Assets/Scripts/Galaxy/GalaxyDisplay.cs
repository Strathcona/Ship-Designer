using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameConstructs;
using System;

public class GalaxyDisplay : MonoBehaviour, IPointerClickHandler {
    public GameObjectPool sectorPool;
    public GameObject sectorPrefab;
    public GameObject galaxyMapRoot;
    public int size = 25;
    public Sector[][] sectors;
    public List<Sector> allSectors = new List<Sector>();
    public GridLayoutGroup layoutGroup;
    public List<Action> onHoverOrSelect = new List<Action>();
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
    public GalaxyData displayedData;

    public void Awake() {
        sectorPool = new GameObjectPool(sectorPrefab, galaxyMapRoot, SetGalaxyTile);
        gradient.alphaKeys = alphaKeys;
        gradient.colorKeys = colorKeys;
    }

    public void DisplayGalaxyData(GalaxyData data) {
        ResetTiles();
        displayedData = data;
        float width = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        float cellSize = width / displayedData.sectors.Length;
        layoutGroup.cellSize = new Vector2(cellSize, cellSize);
        sectors = new Sector[displayedData.sectors.Length][];
        for (int x = 0; x< displayedData.sectors.Length; x++) {
            sectors[x] = new Sector[displayedData.sectors[x].Length];
            for(int y = 0; y < displayedData.sectors[0].Length; y++) {
                GameObject sector = sectorPool.GetGameObject();
                Sector s = sector.GetComponent<Sector>();
                s.baseColor = gradient.Evaluate((float) displayedData.sectors[x][y].systemCount / displayedData.maxCount);
                s.DisplaySector(displayedData.sectors[x][y]);
                s.ShowBaseColor();
                sectors[x][y] = s;
            }
        }
    }

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
        sectors = new Sector[size][];
        gradient.SetKeys(colorKeys, alphaKeys);
    }

    public void TilePointerEnter(Sector tile) {
        HoverSector = tile;
        tile.SetHover(true);
        string displayString = "Major Systems: " + tile.sectorData.systemCount.ToString();
        if (tile.sectorData.Owner != null) {
            displayString += "\nOwner: " + tile.sectorData.Owner.entityName;
        } else {
            displayString += "\nUnclaimed";
        }
        foreach(GalaxyFeature f in tile.sectorData.features) {
            switch (f.featureType) {
                case GalaxyFeatureType.EntityCapital:
                    displayString += "\nCapital System of " + tile.sectorData.Owner.entityName;
                    break;
                default:
                    Debug.LogError("Unsupported feature type on tile pointer enter");
                    break;
            }
        }
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

    public void SectorDisplayUpdate() {
        foreach(Action a in onHoverOrSelect) {
            a();
        }
    }

    private void ResetTiles() {
        sectorPool.ReleaseAll();
    }
}
