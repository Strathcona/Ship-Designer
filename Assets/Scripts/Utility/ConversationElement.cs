using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using GameConstructs;

public class ConversationElement {
    public ConversationElementType type;
    public ConversationElement next;

    public List<string> choices;
    public List<string> responseKeys;
    public List<int> responseValues;

    public string text;

    public string setKey;
    public int setValue;

    public List<List<string>> branchConditions;
    public List<ConversationElement> branchResults;


}
