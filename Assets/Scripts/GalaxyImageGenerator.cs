using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GalaxyImageGenerator : MonoBehaviour
{
    public Image image;
    public int width = 100;
    public int height = 100;
    public Texture2D tex;
    public int buldgeCount = 3000;
    public int armCount = 3000;
    public int numberOfArms = 4;
    public float hubRadius = 15.0f; //radius of buldge
    public float diskRadius = 25.0f; //radius of disk
    public float armRadius = 45.0f; //radius of arms
    public float armWinding = 0.5f; // how tightly the spirals wind
    public float armWidth = 30.0f; // in degrees
    public float armFuzz = 5.0f;
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

    private void Awake() {
        gradient.SetKeys(colorKeys, alphaKeys);
        GenerateGalaxyImage();
    }

    public void GenerateGalaxyImage() {
        tex = new Texture2D(width, height);
        tex.filterMode = FilterMode.Point;
        int[][] counts = new int[width][];
        int maxCount = 0; //used for normalizing
        for(int i = 0; i < width; i++) {
            counts[i] = new int[width];
        }
        for(int i= 0; i < buldgeCount + armCount; i++) {
            Coord c;
            if( i > buldgeCount) {
                c = GetStar(false);
            } else {
                c = GetStar(true);
            }
            counts[c.x][c.y] += 1;
            if(counts[c.x][c.y] > maxCount) {
                maxCount = counts[c.x][c.y];
            }
        }

        Color[] colors = new Color[width * height];
        int x = 0;
        int y = 0;

        for(int i = 0; i < width*height; i++) {
            colors[i] = gradient.Evaluate(counts[x][y] / (float) maxCount);
            x += 1;
            if (x >= width) {
                x = 0;
                y += 1;
            }
        }
        tex.SetPixels(colors);
        tex.Apply();
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = sprite;
    }

    private Coord GetStar(bool buldge) {
        if (buldge) {
            float distance = Random.Range(0.0f, hubRadius);
            float distanceFuzz = distance + Random.Range(-hubRadius/10, hubRadius / 10);

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
            float distanceFuzz = distance + Random.Range(-diskRadius/10 , diskRadius / 10);
            float theta = ((360.0f * armWinding * (distanceFuzz / diskRadius)));
            theta += Random.Range(0.0f, armWidth);
            theta += armDelta * (float)Random.Range(0, numberOfArms);
            theta += Random.Range(0.0f, armFuzz);
            int x = width / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            int y = height / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            return new Coord(x, y);
        }

    }
}
