using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class StringLoader {
    public static string language = "en";
    private static Dictionary<string, List<string>> stringArrays = new Dictionary<string, List<string>>();

    static StringLoader() {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Strings/" + language);
        FileInfo[] files = d.GetFiles("*.txt");
        foreach (FileInfo file in files) {
            string fileName = file.Name.Substring(0, file.Name.Length - 4);
            string[] lines = File.ReadAllLines(file.FullName);
            if (stringArrays.ContainsKey(fileName)) {
                for(int i = 0; i < lines.Length; i++) {
                    stringArrays[fileName].Add(lines[i]);
                }
            } else {
                stringArrays.Add(fileName, new List<string>(lines));
            }
        }
    }


    public static string[] GetAllStrings(string key) {
        if (stringArrays.ContainsKey(key)) {
            return stringArrays[key].ToArray();
        } else {
            Debug.LogError("Couldn't find key " + key + " in StringArrays");
            return new string[1] { "MissingFile" + key };
        }
    }

    public static string GetAString(string key) {
        if (stringArrays.ContainsKey(key)) {
            return stringArrays[key][UnityEngine.Random.Range(0, stringArrays[key].Count -1)];
        } else {
            Debug.LogError("Couldn't find key " + key + " in StringArrays");
            return "MissingFile" + key;
        }
    }

    public static string GetAString(string[] keys) {
        List<string> strings = new List<string>();
        foreach(string key in keys) {
            if (stringArrays.ContainsKey(key)) {
                strings.AddRange(stringArrays[key]);
            } else {
                Debug.LogError("Couldn't find key " + key + " in StringArrays");
                return "";
            }
        }
        if(strings.Count > 0) {
            return strings[UnityEngine.Random.Range(0, strings.Count)];
        } else {
            return "";
        }
    }


}
