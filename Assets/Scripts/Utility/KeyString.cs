using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class KeyString {
    private static Dictionary<string, string> keysAndStrings = new Dictionary<string, string>() {

    };

    static KeyString(){
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Strings/en");
        FileInfo[] files = d.GetFiles("*.txt");
    }

    public static string Get(string key) {
        return keysAndStrings[key];
    }

    public static string GetTense(string key, bool future = true) {
        return keysAndStrings[key];
    }
}
