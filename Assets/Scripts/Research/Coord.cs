using UnityEngine;
using System.Collections;

public struct Coord {
    public int x;
    public int y;

    public Coord(int _x, int _y) {
        x = _x;
        y = _y;
    }

    public bool Equals(Coord other) {
        return (x == other.x && y == other.y);
    }

    public override bool Equals(object obj) {
        if(obj is Coord) {
            return Equals((Coord)obj);
        } else {
            return false;
        }
    }

    public override int GetHashCode() {
        return x.GetHashCode() ^ y.GetHashCode();
    }

    public static bool operator ==(Coord term1, Coord term2) {
        return term1.Equals(term2);
    }

    public static bool operator !=(Coord term1, Coord term2) {
        return !term1.Equals(term2);
    }
}
