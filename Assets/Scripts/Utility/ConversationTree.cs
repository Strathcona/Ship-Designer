using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;
public class ConversationTree {
    public Dictionary<string, bool> conversationVariables = new Dictionary<string, bool>();//name, then value
    public ConversationElement currentElement;
    public IConversationReader currentReader;
    public bool waitingOnChoice;

    public void ProcessNode(ConversationElement element, IConversationReader reader) {
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
                return; //will resume at the end of GetChoiceResponse;
            case ConversationElementType.Branch:

        }   
    }

    public void GetChoiceResponse(int choice) {
        conversationVariables.Add(currentElement.responseKeys[choice], currentElement.responseValues[choice]);
        ProcessNode(currentElement.next, currentReader);
    }

}
