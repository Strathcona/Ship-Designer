using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;
using System.IO;
using System;

public static class SpriteLoader {

    private static Dictionary<string, List<Sprite>> partSprites = new Dictionary<string, List<Sprite>>();
    private static Dictionary<string, List<Sprite>> npcSprites = new Dictionary<string, List<Sprite>>();
    private static Dictionary<string, List<Sprite>> symbolParts = new Dictionary<string, List<Sprite>>();
    private static Dictionary<string, List<Sprite>> featureSprites = new Dictionary<string, List<Sprite>>();
    public readonly static Dictionary<string, Sprite> bitmaskBorderSprites = new Dictionary<string, Sprite>();

    static SpriteLoader() {
        Debug.Log("Loading NPC sprites");
        AddToDictionary(npcSprites, "Images/NPCs");
        Debug.Log("Loading Default Part sprites");
        AddToDictionary(partSprites, "Images/Parts/Default");
        Debug.Log("Loading Company Logos");
        AddToDictionary(symbolParts, "Images/Symbols");
        Debug.Log("Loading Galaxy Feature Icons");
        AddToDictionary(featureSprites, "Images/Features");
        Debug.Log("Loading Border Sprites");
        ConfigureBorderSprites();
    }

    private static void AddToDictionary(Dictionary<string, List<Sprite>> dictionary, string path) {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/"+ path);
        FileInfo[] files = d.GetFiles("*.png");
        int i = 0;
        foreach (FileInfo file in files) {
            string fileName = file.Name.Substring(0, file.Name.Length - 4);
            //trim off the .png
            Sprite s = Resources.Load<Sprite>(path +"/"+ fileName);
            //check if the last digits are "thing_###"
            //We store those together, not in new entries
            //that way when we search "thing" we can get all "thing_001" "thing_002" ...
            if (
            char.IsNumber(fileName[fileName.Length-1]) &&
            char.IsNumber(fileName[fileName.Length - 2]) &&
            char.IsNumber(fileName[fileName.Length - 3]) &&
            fileName[fileName.Length - 4] == '_'){
                string numberlessFileName = fileName.Substring(0, fileName.Length - 4);
                if (dictionary.ContainsKey(numberlessFileName)) {
                    dictionary[numberlessFileName].Add(s);
                } else {
                    dictionary.Add(numberlessFileName, new List<Sprite>() { s });
                }
            } else {
                if (dictionary.ContainsKey(fileName)) {
                    dictionary[fileName].Add(s);
                } else {
                    dictionary.Add(fileName, new List<Sprite>() { s });
                }
            }
            i += 1;
        }
        Debug.Log("Loaded " + i + " sprites");
    }

    public static void ConfigureBorderSprites() {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Images/Border");
        FileInfo[] files = d.GetFiles("*.png");
        foreach (FileInfo file in files) {
            string fileName = file.Name.Substring(0, file.Name.Length - 4);
            Sprite s = Resources.Load<Sprite>("Images/Border/" + fileName);
            bitmaskBorderSprites.Add(fileName, s);
        }
    }

        public static Sprite GetNPCSprite(string name) {
        if (npcSprites.ContainsKey(name)) {
            List<Sprite> sprites = npcSprites[name];
            Sprite toReturn = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            return toReturn;
        } else {
            Debug.LogError("Couldn't find NPC sprite " + name);
            return null;
        }

    }

    public static Sprite GetPartSprite(string name) {
        if (partSprites.ContainsKey(name)) {
            List<Sprite> sprites = partSprites[name];
            Sprite toReturn = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            return toReturn;
        } else {
            Debug.LogError("Couldn't find Part sprite " + name);
            return null;
        }
    }


    public static Sprite GetSymbolPart(string name) {
        if (symbolParts.ContainsKey(name)) {
            List<Sprite> sprites = symbolParts[name];
            Sprite toReturn = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            return toReturn;
        } else {
            Debug.LogError("Couldn't find Symbol sprite " + name);
            return null;
        }
    }

    public static Sprite GetFeatureSprite(string name) {
        if (featureSprites.ContainsKey(name)) {
            List<Sprite> sprites = featureSprites[name];
            Sprite toReturn = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            return toReturn;
        } else {
            Debug.LogError("Couldn't find feature sprite " + name);
            return null;
        }
    }

}
