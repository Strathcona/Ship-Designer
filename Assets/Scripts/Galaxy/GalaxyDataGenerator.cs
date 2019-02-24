using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyDataGenerator : MonoBehaviour {
    public int width = 32;
    public int height = 32;
    public int buldgeCount = 3000;
    public InputFieldIncrement buldgeCountInput;
    public int armCount = 3000;
    public InputFieldIncrement armCountInput;
    public int numberOfArms = 4;
    public InputFieldIncrement numberOfArmsInput;
    public float hubRadius = 7.0f; //radius of buldge
    public float diskRadius = 10.0f; //radius of disk
    public float armRadius = 15.0f; //radius of arms
    public float armWinding = 0.5f; // how tightly the spirals wind
    public float armWidth = 30.0f; // in degrees
    public float fuzzFactor = 15.0f;

}
