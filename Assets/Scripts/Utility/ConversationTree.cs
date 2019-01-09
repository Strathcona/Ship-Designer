using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class ConversationTree {
    public Dictionary<string, bool> conversationVariables = new Dictionary<string, bool>();//name, then value
    public ConversationElement currentElement;
    public ConversationElement nextElement;
    public List<ConversationElement> elements = new List<ConversationElement>();
    public IConversationReader currentReader;
    public bool waitingOnChoice = false;
    public bool endNode = false;

    public static ConversationTree GetTestTree() {
        ConversationTree tree = new ConversationTree();
        tree.elements.Add(new ConversationElement("Well, everything seems to be in order. Would you like to finalize the deal? [UNITS] units at [PRICE] delivered no later than [TIME] ticks from today."));
        tree.elements.Add(new ConversationElement(
            new List<string>() { "Looks like it.", "Hmm, I'm not sure" },
            new List<string>() { "Question", "Question" },
            new List<bool>() { true, false }));
        tree.elements[0].next = 1;
        ConversationElement end = new ConversationElement();
        ConversationElement endTransmission = new ConversationElement(new List<string>() { "End Transmission" }, new List<string>(), new List<bool>());
        ConversationElement agreeText = new ConversationElement("Wonderful. Thank you for your time");
        ConversationElement disagreeText = new ConversationElement("Ah, well, take your time then. Contact us when you know.");
        List<List<string>> branchvar = new List<List<string>>() { new List<string>() { "Agree" }, new List<string>() { "Agree" } };
        List<List<bool>> branchcon = new List<List<bool>>() { new List<bool>() { true }, new List<bool>() { false } };
        ConversationElement agreeBranch = new ConversationElement(branchvar, branchcon, new List<int>() { 3, 4 });
        tree.elements.Add(agreeBranch);
        tree.elements.Add(agreeText);
        tree.elements.Add(disagreeText);
        tree.elements.Add(endTransmission);
        tree.elements.Add(end);
        tree.elements[1].next = 2;
        tree.elements[3].next = 5;
        tree.elements[4].next = 5;
        tree.elements[5].next = 6;


        return tree;
    }

    public void StartTree(ConversationElement element, IConversationReader reader) {
        currentReader = reader;
        ProcessNode(element, reader);   
        if(currentElement.next <= -1) {
            Debug.Log("Tree is one node long");
            currentReader.EndConversation();
        } else if (currentElement.next >= elements.Count) {
            Debug.LogError("Out of range next element");
        } else {
            nextElement = elements[currentElement.next];
        }
    }

    public void NextNode() {
        if (nextElement == null){
            Debug.LogWarning("Conversation ended with null next element "+this);
            currentReader.EndConversation();
        } else if (nextElement.type == ConversationElementType.End) {
            currentReader.EndConversation();
        } else {
            ProcessNode(nextElement, currentReader);
            if (currentElement.next >= elements.Count) {
                Debug.LogError("Out of range next element");
            } else {
                nextElement = elements[currentElement.next];
            }
        }
    }

    private void ProcessNode(ConversationElement element, IConversationReader reader) {
        currentElement = element;
        switch (element.type) {

            case ConversationElementType.Text:
                reader.DisplayText(reader.KeywordReplace(element.text));
                break;

            case ConversationElementType.Set:
                conversationVariables.Add(element.setKey, element.setValue);
                break;

            case ConversationElementType.Choice:
                List<string> keywordedChoices = new List<string>();
                foreach(string choice in element.choices) {
                    keywordedChoices.Add(reader.KeywordReplace(choice));
                }
                reader.DisplayChoice(keywordedChoices, GetChoiceResponse);
                waitingOnChoice = true;
                return;

            case ConversationElementType.Branch:
                for(int i = 0; i < element.branchVariables.Count; i ++) {
                    //i is the branch we're checking
                    bool metCondtions = true;
                    for (int j = 0; j < element.branchVariables[i].Count; j++) {
                        //j is the condition of branch i that we're checking
                        if (conversationVariables.ContainsKey(element.branchVariables[i][j])) {
                            if (conversationVariables[element.branchVariables[i][j]] != element.branchConditions[i][j]) {
                                metCondtions = false;
                            }
                        }
                    }
                    if (metCondtions) {
                        nextElement = elements[element.branchResults[i]];
                        break;
                    }
                }
                //no branch conditions met, so continue to use the next element
                break;
        }
    }

    public void GetChoiceResponse(int choice) {
        if (currentElement.responseKeys.Count > choice) {
            conversationVariables.Add(currentElement.responseKeys[choice], currentElement.responseValues[choice]);
        }
        waitingOnChoice = false;
        ProcessNode(elements[currentElement.next], currentReader);
    }

    public static ConversationTree LoadFromJSON(string json) {
        ConversationTree tree = JsonUtility.FromJson<ConversationTree>(json);
        return tree;
    }

}
