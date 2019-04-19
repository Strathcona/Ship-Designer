﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraTranslateSpeed = 0.1f;
    public float cameraZoomSpeed = 0.1f;
    public float maxX = 3;
    public float maxY = 3;
    public float minX = 0;
    public float minY = 0;
    public float z = 5;
    public float minFoV = 10;
    public float maxFoV = 90;

    private void Awake() {
    }
    private void Update() {
        Vector3 translationVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            translationVector += new Vector3(0, cameraTranslateSpeed, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            translationVector += new Vector3(0, -cameraTranslateSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            translationVector += new Vector3(-cameraTranslateSpeed, 0,0);
        }
        if (Input.GetKey(KeyCode.D)) {
            translationVector += new Vector3(cameraTranslateSpeed, 0,0);
        }
        if(Input.GetAxis("Mouse ScrollWheel") != 0) {
            Camera.main.fieldOfView = Mathf.Min(maxFoV, Mathf.Max(minFoV, Camera.main.fieldOfView + Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed));
        }
        float newX = Mathf.Min(maxX, Mathf.Max(minX, transform.position.x + translationVector.x));
        float newY = Mathf.Min(maxY, Mathf.Max(minY, transform.position.y + translationVector.y));
        transform.position = new Vector3(newX, newY, z);
    }
}
