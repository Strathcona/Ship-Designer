using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class MarkovGenerator {
    private static string end = ".";
    private static Regex awkwardWhitespace = new Regex(@"(?:\s|^|-)\S(?:\s|$|-)");
    private static Regex vowellessWords = new Regex(@"(?:\s|^|-)[b-df-hj-np-tv-wzB-DF-HJ-NP-TV-WZ]+(?:\s|$|-)");
    private static Regex manyConsonants = new Regex("[b-df-hj-np-tv-wzB-DF-HJ-NP-TV-WZ]{4,}");
    private static Regex awkwardConsonants = new Regex("[mngr]{ 3,}");
    private static Regex tripledConsonants = new Regex(@"([b-df-hj-np-tv-wzB-DF-HJ-NP-TV-WZ]+)\1+[b-df-hj-np-tv-wzB-DF-HJ-NP-TV-WZ]|[b-df-hj-np-tv-wzB-DF-HJ-NP-TV-WZ]([b-df-hj-np-tv-wzB-DF-HJ-NP-TV-WZ]+)\2+");

    public static string[] GenerateMarkovWord(string[] words, int numberOfWords, int maxLength=24, int randomize=0) {

        Dictionary<string, Dictionary<string, int> > masterList = new Dictionary<string, Dictionary<string, int>>();
        Dictionary<string, int> startingList = new Dictionary<string, int>();
        
        foreach (string word in words) {
            if(word.Length == 0) {
                continue;
            }
            //get the syllables from the word
            string lastLetter = "";
            for (int i = 0; i < word.Length - 1; i ++) {
                string s = word.Substring(i, 1);

                if (!masterList.ContainsKey(s)) {
                    masterList.Add(s, new Dictionary<string, int>());
                }
                if(lastLetter != "") {
                    if (!masterList[lastLetter].ContainsKey(s)) {
                        masterList[lastLetter].Add(s, 1);
                    } else {
                        masterList[lastLetter][s] += 1;
                    }
                } else {
                    if (!startingList.ContainsKey(s)) {
                        startingList.Add(s, 1);
                    } else {
                        startingList[s] += 1;
                    }
                }
                lastLetter = s;
            }

            if(lastLetter != "") { //it would be for a one letter word
                if (masterList[lastLetter].ContainsKey(".")) {
                    masterList[lastLetter][end] += 1;
                } else {
                    masterList[lastLetter].Add(end, 1);
                }
            }
        }

        //add at least one instance for a little bit of randomization
        if (randomize != 0) {
            foreach (string s in masterList.Keys) {
                foreach (string t in masterList.Keys) {
                    //don't readd starting keys(to prevent odd capitialization) and check if the two letters should never appear together
                    if (!startingList.ContainsKey(t)) {
                        if (!masterList[s].ContainsKey(t)) {
                            masterList[s].Add(t, 1);
                        } else {
                            masterList[s][t] += 1;
                        }
                    }
                }
            }
        }


        string[] toReturn = new string[numberOfWords];
        for(int i = 0; i < numberOfWords; i++) {
            int failCount = 0;

            Restart:
            if(failCount > 100) {
                Debug.LogError("Failed too many times generating word");
                for(int  j = i; j < toReturn.Length; j++) {
                    toReturn[j] = "";
                }
                break;
            }

            string word = GetStringFromDictonaryByWeight(startingList);
            string lastSyllable = word;
            string nextSyllable = "";
            for(int j = 0; j <= maxLength; j++) {
                nextSyllable = GetStringFromDictonaryByWeight(masterList[lastSyllable]);
                if (nextSyllable == end) {
                    break;
                }
                if(lastSyllable == "-" || lastSyllable ==" ") {
                    word += nextSyllable.ToUpper();
                } else {
                    word += nextSyllable;
                }
                lastSyllable = nextSyllable;
                
            }
            if (word.Length <= 2 || awkwardWhitespace.IsMatch(word) || vowellessWords.IsMatch(word) || manyConsonants.IsMatch(word) || awkwardConsonants.IsMatch(word) || tripledConsonants.IsMatch(word)) {
                failCount += 1;
                //Debug.Log(word + " failed regex");
                goto Restart;
            }
            toReturn[i] = word;
        }
        string allWords = "";
        foreach(string word in toReturn) {
            allWords += word + ", ";
        }
        Debug.Log(allWords);

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
