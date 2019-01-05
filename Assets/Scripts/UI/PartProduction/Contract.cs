using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Contract {
    public Part part;
    public bool prototype;
    public bool anyPrice = false;
    private int price; //unit price
    public int Price {
        get { return price; }
        set {
            price = value;
            if (value == -1) {
                anyPrice = true;
            } else {
                anyPrice = false;
            }
        }
    }
    public bool anyUnits = false;
    public int units;
    public bool anyTime = false;
    public int time;

    public Contract(Part p, bool _prototype, int _price = -1 , int _units = -1 , int _time = -1) {
        part = p;
        prototype = _prototype;
        if(_price == -1) {
            anyPrice = true;
        }
        if(_units == -1) {
            anyUnits = true;
        }
        if(_time == -1) {
            anyTime = true;
        }
        price = _price;
        units = _units;
        time = _time;
    }
}
