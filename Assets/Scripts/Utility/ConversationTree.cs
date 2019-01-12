using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;
using GameConstructs;

[System.Serializable]
public class ConversationTree {
    public string name;
    public Dictionary<string, bool> conversationVariables = new Dictionary<string, bool>();//name, then value
    public ConversationElement currentElement;
    public ConversationElement nextElement;
    public List<ConversationElement> elements = new List<ConversationElement>();
    public IConversationReader currentReader;
    public bool waitingOnChoice = false;
    public bool endNode = false;

    public static ConversationTree GetTestTree() {
        return ConversationTreeLoader.GetTree("GenericPartOrderConfirmation.txt");        
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
        Debug.Log("Processing Element " + element.type);
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
            if (conversationVariables.ContainsKey(currentElement.responseKeys[choice])) {
                conversationVariables[currentElement.responseKeys[choice]] = currentElement.responseValues[choice];
            } else {
                conversationVariables.Add(currentElement.responseKeys[choice], currentElement.responseValues[choice]);
            }
        }
        waitingOnChoice = false;
        ProcessNode(elements[currentElement.next], currentReader);
    }

    public static ConversationTree LoadFromJSON(string json) {
        ConversationTree tree = JsonUtility.FromJson<ConversationTree>(json);
        return tree;
    }

}
