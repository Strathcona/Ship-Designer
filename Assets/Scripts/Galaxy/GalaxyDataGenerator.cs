using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyDataGenerator : MonoBehaviour {
    public int size = 32;
    public int buldgeCount = 3000;
    public InputFieldIncrement buldgeCountInput;
    public int armCount = 3000;
    public InputFieldIncrement armCountInput;
    public int numberOfArms = 4;
    public InputFieldIncrement numberOfArmsInput;
    public float hubRadius = 7.0f; //radius of buldge
    public float diskRadius = 10.0f; //radius of disk
    public float armRadius = 15.0f; //radius of arms
    public InputFieldIncrementFloat armWindingInput;
    public float armWinding = 0.5f; // how tightly the spirals wind 
    public InputFieldIncrement armWidthInput;
    public float armWidth = 30.0f; // in degrees
    public float fuzzFactor = 15.0f;

    public GalaxyDisplay previewDisplay;

    public GalaxyData data;

    private void Start() {
        buldgeCountInput.onSubmit.AddListener(GetValuesFromFields);
        armCountInput.onSubmit.AddListener(GetValuesFromFields);
        numberOfArmsInput.onSubmit.AddListener(GetValuesFromFields);
        armWindingInput.onSubmit.AddListener(GetValuesFromFields);
        armWidthInput.onSubmit.AddListener(GetValuesFromFields);
        GetValuesFromFields();
        data = GameDataManager.instance.masterGalaxyData;
    }

    public void GetValuesFromFields() {
        buldgeCount = buldgeCountInput.FieldValue;
        armCount = armCountInput.FieldValue;
        numberOfArms = numberOfArmsInput.FieldValue;
        armWinding = armWindingInput.FieldValue;
        armWidth = armWidthInput.FieldValue;
    }

    public void GenerateGalaxy() {
        data.SetGalaxyData(size);
        int maxCount = 0; //used for normalizing
        float phi = Random.Range(0.0f, 180.0f);//random phase shift
        HashSet<Coord> filledCoords = new HashSet<Coord>();
        for (int i = 0; i < buldgeCount + armCount; i++) {
            Coord c;
            if (i > buldgeCount) {
                c = GetSystem(false, phi);
            } else {
                c = GetSystem(true);
            }
            filledCoords.Add(c);
            if (c.x < size && c.y < size && c.x >= 0 && c.y >= 0) {
                data.sectors[c.x][c.y].systemCount += 1;
                if (data.sectors[c.x][c.y].systemCount > maxCount) {
                    maxCount = data.sectors[c.x][c.y].systemCount;
                }
            }
        }
        data.maxCount = maxCount;
        data.averageCount = (float) (buldgeCount + armCount) / (filledCoords.Count);
        Debug.Log(data.averageCount);
        previewDisplay.DisplayGalaxyData(data);
    }

    private Coord GetSystem(bool buldge, float phi = 0.0f) {
        int x = 0;
        int y = 0;
        if (buldge) {
            float distance = Random.Range(0.0f, hubRadius);
            float distanceFuzz = distance + Random.Range(-hubRadius / 10, hubRadius / 10);

            float theta = Random.Range(0.0f, 360.0f);
            x = size / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            y = size / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            return new Coord(x, y);
        } else {
            //arms
            float armDelta = 0.0f;
            float distance = hubRadius + Random.Range(0.0f, diskRadius);
            float distanceFuzz = distance + Random.Range(-diskRadius / 10, diskRadius / 10);

            if (numberOfArms != 0) {
                armDelta = 360 / numberOfArms;
            } else if (numberOfArms == 0){
                x = size / 2 + Mathf.RoundToInt(Mathf.Cos(Random.Range(0.0f, 360.0f) * Mathf.PI / 180) * Random.Range(0.0f, armRadius));
                y = size / 2 + Mathf.RoundToInt(Mathf.Sin(Random.Range(0.0f, 360.0f) * Mathf.PI / 180) * Random.Range(0.0f, armRadius));
                return new Coord(x, y);
            }

            float theta = ((360.0f * armWinding * (distanceFuzz / diskRadius)));
            theta += Random.Range(0.0f, armWidth);
            theta += armDelta * (float)Random.Range(0, numberOfArms);
            theta += Random.Range(0.0f, fuzzFactor);
            theta += phi;
            x = size / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            y = size / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            return new Coord(x, y);
        }
    }
}
