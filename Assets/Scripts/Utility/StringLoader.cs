using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class StringLoader {
    public static string language = "en";

    private static string[] companyNames;
    private static string[] weaponNames;
    private static string[] shipNames;
    private static string[] fireControlNames;
    private static string[] engineNames;
    private static string[] sensorNames;
    private static string[] reactorNames;
    private static string[] firstNamesMasculine;
    private static string[] firstNamesFeminine;
    private static string[] lastNames;
    private static string[] titles;
    private static string[] entityStrings;
    private static string[] species;

    private static Dictionary<string, string[]> stringArrays;

    static StringLoader() {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Strings/" + language);
        companyNames = File.ReadAllLines(d.FullName+"/CompanyNames.txt");
        weaponNames = File.ReadAllLines(d.FullName + "/WeaponNames.txt");
        shipNames = File.ReadAllLines(d.FullName + "/ShipNames.txt");
        fireControlNames = File.ReadAllLines(d.FullName + "/FCNames.txt");
        engineNames = File.ReadAllLines(d.FullName + "/EngineNames.txt");
        sensorNames = File.ReadAllLines(d.FullName + "/SensorNames.txt");
        reactorNames = File.ReadAllLines(d.FullName + "/PowerPlantNames.txt");
        firstNamesMasculine = File.ReadAllLines(d.FullName + "/FirstNamesMasculine.txt");
        firstNamesFeminine = File.ReadAllLines(d.FullName + "/FirstNamesFeminine.txt");
        lastNames = File.ReadAllLines(d.FullName + "/LastNames.txt");
        titles = File.ReadAllLines(d.FullName + "/Titles.txt");
        entityStrings = File.ReadAllLines(d.FullName + "/EntityStrings.txt");
        species = File.ReadAllLines(d.FullName + "/Species.txt");

        stringArrays = new Dictionary<string, string[]>() {
        {"CompanyNames", companyNames},
        {"weaponNames", weaponNames},
        {"shipNames", shipNames},
        {"fireControlNames", fireControlNames},
        {"engineNames", engineNames},
        {"sensorNames", sensorNames},
        {"reactorNames", reactorNames},
        {"firstNamesMasculine", firstNamesMasculine},
        {"firstNamesFeminine", firstNamesFeminine},
        {"lastNames", lastNames},
        {"titles", titles},
        {"entityStrings", entityStrings},
        {"Species", species}
    };
    }


    public static string[] GetAllStrings(string key) {
        if (stringArrays.ContainsKey(key)) {
            return stringArrays[key];
        } else {
            Debug.LogError("Couldn't find key " + key + " in StringArrays");
            return new string[0];
        }
    }

    public static string GetAString(string key) {
        if (stringArrays.ContainsKey(key)) {
            return stringArrays[key][UnityEngine.Random.Range(0, stringArrays[key].Length -1)];
        } else {
            Debug.LogError("Couldn't find key " + key + " in StringArrays");
            return "";
        }
    }


}
