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
    public bool waitingOnChoice;

    public static ConversationTree GetTestTree() {
        ConversationTree tree = new ConversationTree();
        tree.elements.Add(new ConversationElement("This is a test tree"));
        tree.elements.Add(new ConversationElement(
            new List<string>() { "Looks like it.", "Hmm, I'm not sure" },
            new List<string>() { "Question", "Question" },
            new List<bool>() { true, false }));
        tree.elements[0].next = tree.elements[1];
        tree.elements.Add(new ConversationElement("Another text"));
        tree.elements[1].next = tree.elements[2];
        ConversationElement theEnd = new ConversationElement("The End");
        ConversationElement choiceOne = new ConversationElement("You answered that it looks like it was a test tree!", theEnd);
        ConversationElement choiceTwo = new ConversationElement("You answered that it didn't look like a test tree...", theEnd);
        List<List<string>> branchvar = new List<List<string>>() { new List<string>() { "Question" }, new List<string>() { "Question" } };
        List<List<bool>> branchcon = new List<List<bool>>() { new List<bool>() { true }, new List<bool>() { false } };
        ConversationElement branch = new ConversationElement(branchvar, branchcon, new List<int>() { 4, 5 });
        tree.elements.Add(branch);
        tree.elements[2].next = tree.elements[3];
        tree.elements.Add(choiceOne);
        tree.elements.Add(choiceTwo);
        tree.elements.Add(theEnd);

        return tree;
    }

    public void StartTree(ConversationElement element, IConversationReader reader) {
        ProcessNode(element, reader);   
    }

    public bool NextNode() {
        if(nextElement != null) {
            ProcessNode(nextElement, currentReader);
            return true;
        } else {
            return false;
        }
    }

    private void ProcessNode(ConversationElement element, IConversationReader reader) {
        Debug.Log("Processing a " + element.type + " element");
        currentElement = element;
        nextElement = element.next;
        switch (element.type) {
            case ConversationElementType.Text:
                reader.DisplayText(element.text);
                break;
            case ConversationElementType.Set:
                conversationVariables.Add(element.setKey, element.setValue);
                break;
            case ConversationElementType.Choice:
                reader.DisplayChoice(element.choices, GetChoiceResponse);
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
        conversationVariables.Add(currentElement.responseKeys[choice], currentElement.responseValues[choice]);
        waitingOnChoice = false;
        ProcessNode(currentElement.next, currentReader);
    }

    public static ConversationTree LoadFromJSON(string json) {
        ConversationTree tree = JsonUtility.FromJson<ConversationTree>(json);
        return tree;
    }

}
