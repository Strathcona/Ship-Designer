using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour {
    public static WorldMap instance;
    public float spacing;
    public GameObject root;
    public GameObjectPool sectorObjectPool;
    public GameObject sectorObjectPrefab;
    public Gradient gradient;
    public List<SectorObject> allSectors = new List<SectorObject>();
    private GradientAlphaKey[] a = new GradientAlphaKey[] {
        new GradientAlphaKey(1.0f, 0),
        new GradientAlphaKey(1.0f, 1.0f)
    };
    private GradientColorKey[] c = new GradientColorKey[] {
        new GradientColorKey(Color.black, 0),
        new GradientColorKey(new Color(0.0784f, 0, 0.2924f),0.1f),
        new GradientColorKey(new Color(0.3323f, 0, 0.3723f), 1.0f)
    };
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("There are two world maps...");
        }
        gradient = new Gradient();
        gradient.colorKeys = c;
        gradient.alphaKeys = a;
        sectorObjectPool = new GameObjectPool(sectorObjectPrefab, root);
    }

    public void DisplayGalaxyDataFromMaster() {
        DisplayGalaxyData(GameDataManager.instance.masterGalaxyData);
    }

    public void DisplayGalaxyData(GalaxyData d) {
        allSectors.Clear();
        sectorObjectPool.ReleaseAll();

        float xpos = 0;
        float ypos = 0;

        for (int x = 0; x < d.sectors.Length; x++) {
            ypos = 0;
            for (int y = 0; y < d.sectors[0].Length; y++) {
                GameObject g = sectorObjectPool.GetGameObject();
                g.transform.position = new Vector3(xpos, ypos, 0);
                SectorObject so = g.GetComponent<SectorObject>();
                allSectors.Add(so);
                so.baseColor = gradient.Evaluate((float) d.sectors[x][y].SystemCount / d.maxCount);
                if(d.sectors[x][y].SystemCount == 0) {
                    so.SetTransparent(true);
                } else {
                    so.SetTransparent(false);
                    so.DisplayBaseColor();
                }
                so.DisplaySector(d.sectors[x][y]);
                ypos += spacing;
            }
            xpos += spacing;
        }
    }

    public void ShowTerritory() {
        foreach (SectorObject s in allSectors) {
            s.DisplayOwner();
        }
    }

    public void ShowSystems() {
        foreach (SectorObject s in allSectors) {
            s.DisplayBaseColor();
        }
    }
}
