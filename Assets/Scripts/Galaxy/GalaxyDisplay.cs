using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameConstructs;

public class GalaxyDisplay : MonoBehaviour, IPointerClickHandler {
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
        if(selectedSector != null && selectedSector.sectorData.Owner != null) {
            galaxyEntityPanel.gameObject.SetActive(true);
            galaxyEntityPanel.DisplayEntity(selectedSector.sectorData.Owner);
        } else if (hoverSector != null && hoverSector.sectorData.Owner != null) {
            galaxyEntityPanel.gameObject.SetActive(true);
            galaxyEntityPanel.DisplayEntity(hoverSector.sectorData.Owner);
        } else {
            galaxyEntityPanel.gameObject.SetActive(false);
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
