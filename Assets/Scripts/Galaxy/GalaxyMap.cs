using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GalaxyMap : MonoBehaviour {
    public GameObjectPool galaxyTilePool;
    public GameObject galaxyTilePrefab;
    public int width = 32;
    public int height = 32;

    public GalaxyTile[][] galaxyTiles;
    public List<GalaxyTile> allGalaxyTiles = new List<GalaxyTile>();
    public GridLayoutGroup layoutGroup;

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
        g.GetComponent<GalaxyTile>().map = this;
    }

    public void ShowTerritory() {
        foreach(GalaxyTile t in allGalaxyTiles) {
            t.ShowOwnerColor();
        }
    }

    public void ShowSystems() {
        foreach (GalaxyTile t in allGalaxyTiles) {
            t.ShowBaseColor();
        }
    }
    
    public void Initialize() {
        galaxyTilePool = new GameObjectPool(galaxyTilePrefab, gameObject, SetGalaxyTile);
        galaxyTiles = new GalaxyTile[width][];
        for(int i = 0; i < width; i++) {
            galaxyTiles[i] = new GalaxyTile[height];
            for(int j = 0; j < height; j++) {
                GameObject g = galaxyTilePool.GetGameObject();
                GalaxyTile t = g.GetComponent<GalaxyTile>();
                t.coord = new Coord(i, j);
                galaxyTiles[i][j] = t;
                allGalaxyTiles.Add(t);
            }
        }
        //precompute Neighbours
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                List<GalaxyTile> neighbours = new List<GalaxyTile>();
                if(i + 1 < width) {
                    neighbours.Add(galaxyTiles[i + 1][j]);
                }
                if (i - 1 >= 0) {
                    neighbours.Add(galaxyTiles[i - 1][j]);
                }
                if (j + 1 < height) {
                    neighbours.Add(galaxyTiles[i][j + 1]);
                }
                if (j - 1 >= 0) {
                    neighbours.Add(galaxyTiles[i][j - 1]);
                }
                GalaxyTile[] pass = neighbours.ToArray();
                galaxyTiles[i][j].neighbours = pass;
                neighbours.Clear();
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
                galaxyTiles[c.x][c.y].systemCount += 1;
                if (galaxyTiles[c.x][c.y].systemCount > maxCount) {
                    maxCount = galaxyTiles[c.x][c.y].systemCount;
                }
            }
        }

        int x = 0;
        int y = 0;

        for(int i = 0; i < width*height; i++) {
            galaxyTiles[x][y].baseColor = gradient.Evaluate(galaxyTiles[x][y].systemCount / (float) maxCount);
            galaxyTiles[x][y].ShowBaseColor();
            x += 1;
            if (x >= width) {
                x = 0;
                y += 1;
            }
        }
    }

    public void TilePointerEnter(GalaxyTile tile) {
        galaxyTileDescription.text = "Major Systems: " + tile.systemCount.ToString();
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
                galaxyTiles[i][j].Clear();
            }
        }
    }
}
