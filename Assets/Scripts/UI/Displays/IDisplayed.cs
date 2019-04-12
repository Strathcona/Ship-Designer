using UnityEngine;
using System.Collections;
using System;

public interface IDisplayed {
    event Action<IDisplayed> DisplayUpdateEvent;
    string[] DisplayStrings { get; }
    LayeredColoredSprite[] DisplaySprites { get; }
}
