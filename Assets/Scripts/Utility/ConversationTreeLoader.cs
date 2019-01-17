using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

public static class ConversationTreeLoader {
    private static Dictionary<string, List<ConversationTree>> allTrees = new Dictionary<string, List<ConversationTree>>();

    static ConversationTreeLoader() {
        Debug.Log("Loading Conversations");
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Text/Conversations");
        FileInfo[] files = d.GetFiles("*.txt");
        List<ConversationTree> trees = new List<ConversationTree>();
        int count = 0;
        foreach (FileInfo file in files) {
            count += 1;
            ConversationTree tree = new ConversationTree();
            List<ConversationElement> elements = new List<ConversationElement>();
            string[] lines = File.ReadAllLines(file.FullName);
            for (int i = 0; i < lines.Length; i++) {
                if (lines[i].StartsWith("#")) {
                    continue;
                } else if (lines[i].Length == 0) {
                    continue;
                }
                List<string> topLevelStrings = SplitOnTopLevelBrackets(lines[i]);

                if(topLevelStrings[0] == "END") {
                    elements.Add(new ConversationElement());
                    continue;
                }

                int next = -1;
                int.TryParse(topLevelStrings[1], out next);
                switch (topLevelStrings[0]) {
                    case "TEXT":
                        elements.Add(new ConversationElement(topLevelStrings[2], next));
                        break;
                    case "CHOICE":
                        List<string> choices = SplitCommaSeperatedList(topLevelStrings[2]);
                        List<string> responseKeys = SplitCommaSeperatedList(topLevelStrings[3]);
                        List<bool> responseValues = new List<bool>();
                        foreach(string s in SplitCommaSeperatedList(topLevelStrings[4])) {
                            if(s == "true") {
                                responseValues.Add(true);
                            } else {
                                responseValues.Add(false);
                            }
                        }
                        elements.Add(new ConversationElement(choices, responseKeys, responseValues, next));
                        break;
                    case "BRANCH":
                        List<List<string>> branchVariables = new List<List<string>>();
                        foreach(string s in SplitOnTopLevelBrackets(topLevelStrings[2])) {
                            branchVariables.Add(SplitCommaSeperatedList(s));
                        }

                        List<List<bool>> branchConditions = new List<List<bool>>();
                        foreach (string s in SplitOnTopLevelBrackets(topLevelStrings[3])) {
                            List<bool> cons = new List<bool>();
                            foreach(string b in SplitCommaSeperatedList(s)) {
                                if (s == "true") {
                                    cons.Add(true);
                                } else {
                                    cons.Add(false);
                                }
                            }
                            branchConditions.Add(cons);
                        }

                        List<int> branchDestinations = new List<int>();
                        foreach(string s in SplitCommaSeperatedList(topLevelStrings[4])) {
                            int destination = -1;
                            int.TryParse(s, out destination);
                            branchDestinations.Add(destination);
                        }
                        elements.Add(new ConversationElement(branchVariables, branchConditions, branchDestinations, next));
                        break;
                    default:
                        Debug.LogError("Couldn't parse conversation key " + topLevelStrings[0]+" in "+file.Name);
                        break;
                }
            }
            tree.elements = elements;

            string fileName = file.Name.Substring(0, file.Name.Length - 4); //trim off the .txt;

            //check if the last digits are "thing_###"
            //We store those together, not in new entries
            //that way when we search "thing" we can get all "thing_001" "thing_002" ...
            if (
            char.IsNumber(fileName[fileName.Length - 1]) &&
            char.IsNumber(fileName[fileName.Length - 2]) &&
            char.IsNumber(fileName[fileName.Length - 3]) &&
            fileName[fileName.Length - 4] == '_') {
                string numberlessFileName = fileName.Substring(0, fileName.Length - 4);
                if (allTrees.ContainsKey(numberlessFileName)) {
                    allTrees[numberlessFileName].Add(tree);
                } else {
                    allTrees.Add(numberlessFileName, new List<ConversationTree>() { tree });
                }
            } else {
                if (allTrees.ContainsKey(fileName)) {
                    allTrees[fileName].Add(tree);
                } else {
                    allTrees.Add(fileName, new List<ConversationTree>() { tree });
                }
            }
        }
        Debug.Log("Loaded " + count + " files");
    }

    public static ConversationTree GetTree(string name) {
        if (allTrees.ContainsKey(name)) {
            List<ConversationTree> trees = allTrees[name];
            return trees[Random.Range(0, trees.Count)];
        } else {
            Debug.LogError("Couldn't find conversation tree" + name);
            return null;
        }
    }

    private static List<string> SplitCommaSeperatedList(string s) {
        bool quote = false;
        bool escape = false;
        List<string> strings = new List<string>();
        StringBuilder currentString = new StringBuilder();
        foreach (char c in s) {
            if (quote) { //if we're inside a quote, we ignore everything except for escape characters and the end quote
                if (c == '/' && escape != true) {
                    escape = true;
                }
                if (escape == true) {//if we're escaped, the next character is added even if it's a quote
                    currentString.Append(c);
                    escape = false;
                } else {
                    if (c == '"') {
                        quote = false; //we're out of the quote
                    } else {
                        currentString.Append(c); // if it's not a closing quote, add it to the list
                    }
                }
            } else if (c == '"') {
                quote = true;
            } else if (c == ',') {
                    strings.Add(currentString.ToString());
                    currentString.Clear();
            } else {
                currentString.Append(c);
            }
        }
        strings.Add(currentString.ToString());
        return strings;
    }

    private static List<string> SplitOnTopLevelBrackets(string s) {
        bool quote = false;
        bool escape = false;
        int nestedBrackets = 0;
        List<string> strings = new List<string>();
        StringBuilder currentString = new StringBuilder();
        foreach (char c in s) {
            if (quote) { //if we're inside a quote, we ignore everything except for escape characters and the end quote
                if (c == '/' && escape != true) {
                    escape = true;
                }
                if (escape == true) {//if we're escaped, the next character is added even if it's a quote
                    currentString.Append(c);
                    escape = false;
                } else {
                    if (c == '"') {
                        quote = false; //we're out of the quote
                        currentString.Append(c);
                    } else {
                        currentString.Append(c); // if it's not a closing quote, add it to the list
                    }
                }
            } else {
                if (c == ']') {
                    nestedBrackets -= 1;
                    if (nestedBrackets == 0) { //we're back at top level, store the string
                        strings.Add(currentString.ToString());
                        currentString.Clear();
                    } else {
                        currentString.Append(c);
                    }
                } else if (c == '[') { //head one deeper
                    nestedBrackets += 1;
                    if(nestedBrackets > 1) {
                        currentString.Append(c);
                    }
                } else if (c == '"') {
                    quote = true;
                    currentString.Append(c);
                } else {
                    currentString.Append(c);
                }
            }
        }
        return strings;
    }
}
