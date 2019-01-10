using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public static class ConversationTreeLoader {
    private static List<ConversationTree> allTrees;

    static ConversationTreeLoader() {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/Text/Conversations");
        FileInfo[] files = d.GetFiles("*.txt");
        List<ConversationTree> trees = new List<ConversationTree>();
        foreach (FileInfo file in files) {
            ConversationTree tree = new ConversationTree();
            List<ConversationElement> elements = new List<ConversationElement>();
            tree.name = file.Name;
            string[] lines = File.ReadAllLines(file.FullName);
            for (int i = 0; i < lines.Length; i++) {
                if (lines[i].StartsWith("#")) {
                    continue;
                } else if (lines[i].Length == 0) {
                    continue;
                }
                string[] split = lines[i].Split(':');
                string elementType = split[0];
                int next = -1;
                if (!int.TryParse(split[1], out next)) {
                    Debug.LogError("Cannot parse int on line " + i + " of " + file.FullName);
                }
                switch (elementType) {
                    case "TEXT":
                        string text = split[2];
                        elements.Add(new ConversationElement(text, next));
                        break;
                    case "CHOICE":
                        List<string> choices = new List<string>();
                        foreach(string s in split[2].Split(',')) {
                            choices.Add(s);
                        }
                        List<string> responseKeys = new List<string>();
                        foreach(string s in split[3].Split(',')) {
                            responseKeys.Add(s);
                        }
                        List<bool> responseValues = new List<bool>();
                        foreach(string s in split[4].Split(',')) {
                            if(s == "true") {
                                responseValues.Add(true);
                            } else {
                                responseValues.Add(false);
                            }
                        }
                        elements.Add(new ConversationElement(choices, responseKeys, responseValues, next));
                        break;
                    case "BRANCH":
                        break;
                }
            }

        }

    }


}
