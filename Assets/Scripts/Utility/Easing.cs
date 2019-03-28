using UnityEngine;
using System.Collections;

public static class Easing {

    public static float QuadradicUp(float x) {
        return x * x;
    }

    public static float QuadradicTop(float x) {
        return -(x * (x - 2));
        //negative quadradic, poles at 0, 2, peak at 1
    }

    public static float QuadradicDown(float x) {
        return -(x * x) + 1;
        //negative quadradic, poles at +1 -1, peak at 0;
    }

    public static float QuadradicBottom(float x) {
        return (x * (x - 2)) + 1;
        //positive quadradic, one pole at 1
    }
}
