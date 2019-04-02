using UnityEngine;
using System.Collections.Generic;
using System;
public static class MarkovGenerator {
    private static int maxSyllables = 7;

    public static string[] GenerateMarkovWord(string[] words, int syllableLength, int numberOfWords) {
        SyllableList syllableList = new SyllableList();
        foreach (string word in words) {
            if(word.Length == 0) {
                continue;
            }
            //pad the word so we can break it down evenly
            string paddedWord = word;
            int syllablesInWord = word.Length % syllableLength;
            if (syllablesInWord == 0) {
                paddedWord += new string('.', syllableLength);
            } else {
                paddedWord += new string('.', syllablesInWord);
            }
            //get the syllables from the word
            Syllable lastSyllable = null;
            for (int i = 0; i < paddedWord.Length; i+= syllableLength) {
                Syllable s = new Syllable(paddedWord.Substring(i, syllableLength));
                if(lastSyllable != null) {
                    lastSyllable.following.AddSyllable(s);
                }
                syllableList.AddSyllable(s);
                lastSyllable = s;
            }
        }

        string[] toReturn = new string[numberOfWords];
        for(int i = 0; i < numberOfWords; i++) {
            string word = "";
            Syllable syllable = syllableList.GetWeightedSyllable();
            for(int j = 0; j < maxSyllables; j++) {
                word += syllable.characters;
                if (word.Contains(".")) {
                    while (word.Contains(".")) {
                        word.Remove(word.Length - 1);
                    }
                    break;
                }
                syllable = syllable.following.GetWeightedSyllable();
            }
            toReturn[i] = word;
        }
        return toReturn;
    }

    private class Syllable {
        public Syllable(string characters) {
            this.characters = characters;
        }

        public string characters;
        public int instances;
        public SyllableList following = new SyllableList();
    }

    private class SyllableList {
        public List<Syllable> syllables = new List<Syllable>();
        public int totalInstances = 0;

        public Syllable GetWeightedSyllable() {
            int index = 0;
            int alpha = 0;

            int random = UnityEngine.Random.Range(0, totalInstances);
            while(alpha <= totalInstances) {
                if(index >= syllables.Count) {
                    //something went wrong
                    break;
                }
                alpha += syllables[index].instances;
                if (alpha > random) {
                    return (syllables[index]);
                } else {
                    index += 1;
                }
            }
            Debug.LogError("Markov generator ran out of syllables when getting next");
            return null;
        }

        public void AddSyllable(Syllable s) {
            Syllable existingSyllable = syllables.Find(i => i.characters == s.characters);
            if (existingSyllable == null){
                s.instances = 1;
                totalInstances += 1;
                syllables.Add(s);
            } else {
                existingSyllable.instances += 1;
                totalInstances += 1;
            }
        }
    }
}
