using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;
using System;

public class Hardpoint {
    public PartType allowableType;
    public Orientation orientation;
    public int allowableSize;
    public Part part;
    public Action onChange;

    public bool MountPart(Part p) {
        if (allowableType == p.partType) {
            if(p.Size == allowableSize) {
                part = p;
                onChange();
                return true;
            }
            return false;
        }
        return false;
    }

    public Part UnmountPart() {
        Part p = part;
        part = null;
        onChange();
        return p;
    }

    public Hardpoint(int _allowableSize, PartType _allowableType, Orientation _orientation) {
        allowableSize = _allowableSize;
        allowableType = _allowableType;
        orientation = _orientation;
    }
    
}
