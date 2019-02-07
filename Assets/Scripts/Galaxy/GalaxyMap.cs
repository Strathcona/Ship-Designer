using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GalaxyMap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObjectPool galaxyTilePool;
    public GameObject galaxyTilePrefab;
    public int width = 32;
    public int height = 32;

    public GalaxyTile[][] galaxyTiles;
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

    public bool hovering = false;

    public void OnPointerEnter(PointerEventData data) {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData data) {
        hovering = false;
    }

    private void Awake() {
        galaxyTilePool = new GameObjectPool(galaxyTilePrefab, gameObject);
        galaxyTiles = new GalaxyTile[width][];
        for(int i = 0; i < width; i++) {
            galaxyTiles[i] = new GalaxyTile[height];
            for(int j = 0; j < height; j++) {
                GameObject g = galaxyTilePool.GetGameObject();
                galaxyTiles[i][j] = g.GetComponent<GalaxyTile>();
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
                galaxyTiles[i][j].neighbours = neighbours;
            }
        }

        gradient.SetKeys(colorKeys, alphaKeys);
        GenerateGalaxy();
        
    }

    private void Update() {
        if (hovering) {

        }
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
                galaxyTiles[c.x][c.y].starCount += 1;
                if (galaxyTiles[c.x][c.y].starCount > maxCount) {
                    maxCount = galaxyTiles[c.x][c.y].starCount;
                }
            }
        }

        int x = 0;
        int y = 0;

        for(int i = 0; i < width*height; i++) {
            galaxyTiles[x][y].SetBGColor(gradient.Evaluate(galaxyTiles[x][y].starCount / (float) maxCount));
            x += 1;
            if (x >= width) {
                x = 0;
                y += 1;
            }
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
                galaxyTiles[i][j].Clear();
            }
        }
    }
}
