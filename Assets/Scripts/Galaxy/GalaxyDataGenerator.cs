﻿using System.Collections;
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
    public float armWidth = 30.0f; // in degrees
    public float fuzzFactor = 15.0f;

    public GalaxyDisplay previewDisplay;

    public GalaxyData data;

    private void Awake() {
        buldgeCountInput.onSubmit.AddListener(GetValuesFromFields);
        armCountInput.onSubmit.AddListener(GetValuesFromFields);
        numberOfArmsInput.onSubmit.AddListener(GetValuesFromFields);
        armWindingInput.onSubmit.AddListener(GetValuesFromFields);
        GetValuesFromFields();
    }

    public void GetValuesFromFields() {
        buldgeCount = buldgeCountInput.FieldValue;
        armCount = armCountInput.FieldValue;
        numberOfArms = numberOfArmsInput.FieldValue;
        armWinding = armWindingInput.FieldValue;
    }

    public void GenerateGalaxy() {
        GalaxyData newData = new GalaxyData(size);
        int maxCount = 0; //used for normalizing
        for (int i = 0; i < buldgeCount + armCount; i++) {
            Coord c;
            if (i > buldgeCount) {
                c = GetSystem(false);
            } else {
                c = GetSystem(true);
            }
            if (c.x < size && c.y < size && c.x >= 0 && c.y >= 0) {
                newData.sectors[c.x][c.y].systemCount += 1;
                if (newData.sectors[c.x][c.y].systemCount > maxCount) {
                    maxCount = newData.sectors[c.x][c.y].systemCount;
                }
            }
        }
        newData.maxCount = maxCount;
        data = newData;
        previewDisplay.DisplayGalaxyData(data);
    }

    private Coord GetSystem(bool buldge) {
        if (buldge) {
            float distance = Random.Range(0.0f, hubRadius);
            float distanceFuzz = distance + Random.Range(-hubRadius / 10, hubRadius / 10);

            float theta = Random.Range(0.0f, 360.0f);
            int x = size / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            int y = size / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            return new Coord(x, y);
        } else {
            //arms
            float armDelta = 0.0f;
            if (numberOfArms != 0) {
                armDelta = 360 / numberOfArms;
            }
            float distance = hubRadius + Random.Range(0.0f, diskRadius);
            float distanceFuzz = distance + Random.Range(-diskRadius / 10, diskRadius / 10);
            float theta = ((360.0f * armWinding * (distanceFuzz / diskRadius)));
            theta += Random.Range(0.0f, armWidth);
            theta += armDelta * (float)Random.Range(0, numberOfArms);
            theta += Random.Range(0.0f, fuzzFactor);
            int x = size / 2 + Mathf.RoundToInt(Mathf.Cos(theta * Mathf.PI / 180) * distanceFuzz);
            int y = size / 2 + Mathf.RoundToInt(Mathf.Sin(theta * Mathf.PI / 180) * distanceFuzz);
            return new Coord(x, y);
        }
    }
}