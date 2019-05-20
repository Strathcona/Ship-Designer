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
        data.Initialize(size);
        int maxCount = 0; //used for normalizing
        HashSet<Coord> filledCoords = new HashSet<Coord>();
        float phi = Random.Range(0.0f, 180.0f);//random phase shift
        //buldge
        for (int i = 0; i < buldgeCount; i++){
            float distance = Random.Range(0.0f, hubRadius);
            float distanceFuzz = distance + Random.Range(-hubRadius / 10, hubRadius / 10);
            float theta = Random.Range(0.0f, 360.0f);
            int x = size / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            int y = size / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            if (x < size && y < size && x >= 0 && y >= 0) {
                data.sectors[x][y].SystemCount += 1;
                filledCoords.Add(new Coord(x, y));
                if (data.sectors[x][y].SystemCount > maxCount) {
                    maxCount = data.sectors[x][y].SystemCount;
                }
            }
        }
        //if we've got no arms it's just a big circle
        if (armCount <= 0) {
            for (int i = 0; i < armCount; i++) {
                float distance = Random.Range(0.0f, diskRadius);
                float distanceFuzz = distance + Random.Range(-diskRadius / 10, diskRadius / 10);
                float theta = Random.Range(0.0f, 360.0f);
                int x = size / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
                int y = size / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
                if (x < size && y < size && x >= 0 && y >= 0) {
                    data.sectors[x][y].SystemCount += 1;
                    filledCoords.Add(new Coord(x, y));
                    if (data.sectors[x][y].SystemCount > maxCount) {
                        maxCount = data.sectors[x][y].SystemCount;
                    }
                }
            }
        } else {
            //arms
            for (int i = 0; i < armCount; i++) {
                float armDelta = 0.0f;
                float distance = hubRadius + Random.Range(0.0f, diskRadius); //where along the arm it is
                float distanceFuzz = distance + Random.Range(-diskRadius / 10, diskRadius / 10);
                armDelta = 360 / numberOfArms;
                float theta = ((360.0f * armWinding * (distanceFuzz / diskRadius)));
                theta += Random.Range(0.0f, armWidth); //where across the arm it is
                theta += armDelta * (float)Random.Range(0, numberOfArms); //which arm it is
                theta += Random.Range(0.0f, fuzzFactor); //fuzz
                theta += phi; //phase shift
                int x = size / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
                int y = size / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
                if (x < size && y < size && x >= 0 && y >= 0) {
                    data.sectors[x][y].SystemCount += 1;
                    filledCoords.Add(new Coord(x, y));
                    if (data.sectors[x][y].SystemCount > maxCount) {
                        maxCount = data.sectors[x][y].SystemCount;
                        Debug.Log("Placing Arm at " + x + " " + y);
                    }
                }
            }
        }
        data.maxCount = maxCount;
        data.averageCount = (float) (buldgeCount + armCount) / (filledCoords.Count);
        WorldMap.instance.DisplayGalaxyData(data);
    }
}
