using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Hardpoint {
    public HashSet<PartType> allowableTypes = new HashSet<PartType>();
    public int allowableSize;
    public bool external;
    public Part part;

    public bool MountPart(Part p) {
        if (allowableTypes.Contains(p.partType)) {
            if(p.Size == allowableSize) {
                part = p;
                return true;
            }
            return false;
        }
        return false;
    }

    public Part UnmountPart() {
        Part p = part;
        part = null;
        return p;
    }

    public Hardpoint(int _allowableSize, HashSet<PartType> _allowableTypes, bool _external) {
        allowableSize = _allowableSize;
        allowableTypes = _allowableTypes;
        external = _external;
    }
    
}
