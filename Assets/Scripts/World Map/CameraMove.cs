using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraTranslateSpeed = 0.02f;
    public float maxX = 3;
    public float maxY = 3;
    public float minX = 0;
    public float minY = 0;
    public float minZ = -1;

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
        float newX = Mathf.Min(maxX, Mathf.Max(minX, transform.position.x + translationVector.x));
        float newY = Mathf.Min(maxY, Mathf.Max(minY, transform.position.y + translationVector.y));
        transform.position = new Vector3(newX, newY, minZ);
    }
}
