using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Contract {
    public Part part;
    public bool prototype;
    public int price;
    public int units;
    public int time;

    public Contract(Part p, bool _prototype = false, int _price = -1 , int _units = -1 , int _time = -1) {
        part = p;
        prototype = _prototype;
        price = _price;
        units = _units;
        time = _time;
    }
}
