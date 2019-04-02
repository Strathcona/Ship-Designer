using UnityEngine;
using System.Collections.Generic;
using System;
public static class MarkovGenerator {
    private static string end = ".";

    public static string[] GenerateMarkovWord(string[] words, int syllableLength, int numberOfWords) {

        Dictionary<string, Dictionary<string, int> > masterList = new Dictionary<string, Dictionary<string, int>>();
        Dictionary<string, int> startingList = new Dictionary<string, int>();
        
        foreach (string word in words) {
            if(word.Length == 0) {
                continue;
            }
            //pad the word so we can break it down evenly
            int syllablesInWord = word.Length % syllableLength;
            if (syllablesInWord != 0) {
                syllablesInWord += 1;
            }
            //get the syllables from the word
            string lastSyllable = "";
            for (int i = 0; i < word.Length; i += syllableLength) {
                string s;
                if( i + syllableLength > word.Length) {
                    s = word.Substring(i, word.Length - i);
                } else {
                    s = word.Substring(i, syllableLength);
                }
                if (!masterList.ContainsKey(s)) {
                    masterList.Add(s, new Dictionary<string, int>());
                }
                if(lastSyllable != "") {
                    if (!masterList[lastSyllable].ContainsKey(s)) {
                        masterList[lastSyllable].Add(s, 1);
                    } else {
                        masterList[lastSyllable][s] += 1;
                    }
                } else {
                    if (!startingList.ContainsKey(s)) {
                        startingList.Add(s, 1);
                    } else {
                        startingList[s] += 1;
                    }
                }
                lastSyllable = s;
            }

            if (masterList[lastSyllable].ContainsKey(".")) {
                masterList[lastSyllable]["."] += 1;
            } else {
                masterList[lastSyllable].Add(".", 1);
            }
        }

        //add at least one instance for a little bit of randomization
        foreach(string s in masterList.Keys) {
            foreach(string t in masterList.Keys) {
                //don't readd starting keys
                if (!startingList.ContainsKey(t)) {
                    if (!masterList[s].ContainsKey(t)) {
                        masterList[s].Add(t, 1);
                    } else {
                        masterList[s][t] += 1;
                    }
                }
            }
        }

        Debug.Log("Generating Return");
        string[] toReturn = new string[numberOfWords];
        for(int i = 0; i < numberOfWords; i++) {
            string word = GetStringFromDictonaryByWeight(startingList);
            string lastSyllable = word;
            for(int j = 0; j < 100; j++) {
                lastSyllable = GetStringFromDictonaryByWeight(masterList[lastSyllable]);
                if (lastSyllable == ".") {
                    break;
                }
                word += lastSyllable;

            }
            toReturn[i] = word;
        }
        return toReturn;
    }

    private static string GetStringFromDictonaryByWeight(Dictionary<string, int> dict) {
        int total = 0;
        foreach(int k in dict.Values) {
            total += k;
        }
        int alpha = 0;
        int random = UnityEngine.Random.Range(0, total);
        foreach (string s in dict.Keys) {
            alpha += dict[s];
            if(alpha > random) {
                return s;
            }
        }
        Debug.LogError("Didn't get string from dict");
        return "Something Messed Up";
    }
}
