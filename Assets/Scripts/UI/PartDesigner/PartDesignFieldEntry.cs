using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public abstract class PartDesignFieldEntry : MonoBehaviour {
    public abstract void Initialize(Part p);
    public abstract void Clear();
    public abstract void SetPart();
    public abstract Part GetPart();

    protected virtual void UpdateStrings() {
        updateStringsCallback();
    }
    public abstract PartType GetPartType();
    protected Action updateStringsCallback;

    public void SetUpdateStringsCallback(Action action) {
        updateStringsCallback = action;
    }
}
