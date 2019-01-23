using UnityEngine;
using System.Collections;

public class PartSupplyAgreement {
    public Part part;
    public int price;
    public int time;
    public string comment;

    public PartSupplyAgreement(Part _part, int _price, int _time) {
        part = _part;
        price = _price;
        time = _time;
    }
}
