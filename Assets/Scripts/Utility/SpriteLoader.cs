﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;
using System.IO;
using System;

public static class SpriteLoader {

    private static Dictionary<string, List<Sprite>> partSprites = new Dictionary<string, List<Sprite>>();
    private static Dictionary<string, List<Sprite>> alienSprites = new Dictionary<string, List<Sprite>>();
    private static Dictionary<string, List<Sprite>> symbolParts = new Dictionary<string, List<Sprite>>();
    private static Dictionary<string, List<Sprite>> featureSprites = new Dictionary<string, List<Sprite>>();
    public readonly static Dictionary<string, Sprite> bitmaskBorderSprites = new Dictionary<string, Sprite>();

    static SpriteLoader() {
        Debug.Log("Loading NPC sprites");
        AddToDictionary(alienSprites, "Images/Aliens");
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
            if(s == null) {
                Debug.LogError("Loaded Null Sprite "+fileName);
            }
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

    public static Sprite GetAlienSprite(string name) {
        if (alienSprites.ContainsKey(name)) {
            List<Sprite> sprites = alienSprites[name];
            Sprite toReturn = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            return toReturn;
        } else {
            Debug.LogError("Couldn't find NPC sprite " + name);
            return null;
        }

    }

    public static List<Sprite> GetSetOfAlienSprites(string name) {
        if (alienSprites.ContainsKey(name)) {
            return alienSprites[name];
        } else {
            Debug.LogError("Couldn't find NPC sprites " + name);
            return null;
        }
    }

    public static List<Sprite> GetRandomSetOfAlienSprites() {
        if(alienSprites.Count >= 1) {
            int num = UnityEngine.Random.Range(0, alienSprites.Count);
            int index = 0;
            foreach (List<Sprite> s in alienSprites.Values) {
                if(num == index) {
                    return s;
                }
                index += 1;
            }
        }
        Debug.LogError("Couldn't find a set of Alien Sprites");
        return null;
    }

    public static List<List<Sprite>> GetAllAlienSprites() {
        List<List<Sprite>> allSprites = new List<List<Sprite>>();
        foreach(List<Sprite> s in alienSprites.Values) {
            allSprites.Add(s);
        }
        return allSprites;
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

    public static List<Sprite> GetAllSymbolParts(string name) {
        if (symbolParts.ContainsKey(name)) {
            return symbolParts[name];
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
