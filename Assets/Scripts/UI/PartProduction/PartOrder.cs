using UnityEngine;
using System.Collections;

public class PartOrder {
    public Part part;
    public int units;
    public int price;
    public int time;
    public bool prototype;
    public string proposalString;

    public PartOrder(Part _part, int _units, int _price, int _time, bool _prototype) {
        part = _part;
        units = _units;
        price = _price;
        time = _time;
        prototype = _prototype;
    }
}
