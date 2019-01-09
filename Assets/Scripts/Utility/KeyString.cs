using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class KeyString {
    private static Dictionary<string, string> keysAndStrings = new Dictionary<string, string>() {
        {"PartOrderSimple", "This seems to be well withing our capabilities." }
    };
    
    public static string GetStringFromKey(string key) {
        return keysAndStrings[key];
    }
}
