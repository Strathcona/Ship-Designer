using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PartDesignFieldEntry : MonoBehaviour {
    public abstract void Initialize();
    public abstract void Clear();
    protected virtual void UpdateStrings() {
        updateStringsCallback();
    }
    public abstract Part GetPart();
    protected Action updateStringsCallback;



    public void SetUpdateStringsCallback(Action action) {
        updateStringsCallback = action;
    }
}
