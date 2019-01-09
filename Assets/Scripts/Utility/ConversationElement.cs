using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using GameConstructs;

[System.Serializable]
public class ConversationElement {
    public ConversationElementType type;
    public int next;
    public List<string> choices;
    public List<string> responseKeys;
    public List<bool> responseValues;
    //choices is the strings to present the player
    //response keys correspond to each choice, setting a variable in the Dict in the tree
    //response values populate that key with the value from the choice, if chosen
    //choices not taken just don't get added to the dict at all and the variables don't show

    public string text;
    //text to display

    public string setKey;
    public bool setValue;

    public List<List<string>> branchVariables;
    //the name of the variables you want to check per branch
    public List<List<bool>> branchConditions;
    //the values you want the variables to be
    public List<int> branchResults;
    //where you want to go if the values are all correct
    //refers to the index of the conversation element list kept in the tree;
    //if multiple are true, you just go to the first correct one
    //if none are, you just go to the next element as set in next

    public ConversationElement(string _text, int _next = -1) {
        ///<summary>
        ///Create a Text Element
        ///</summary>
        text = _text;
        next = _next;
        type = ConversationElementType.Text;
    }

    public ConversationElement(string _setKey, bool _setValue, int _next = -1) {
        ///<summary>
        ///Create a Set Element
        ///</summary>
        setKey = _setKey;
        setValue = _setValue;
        type = ConversationElementType.Set;
    }

    public ConversationElement(List<string> _choices, List<string> _responseKeys, List<bool> _responseValues, int _next =-1) {
        ///<summary>
        ///Create a Choice Element
        ///</summary>
        choices = _choices;
        responseKeys = _responseKeys;
        responseValues = _responseValues;
        next = _next;
        type = ConversationElementType.Choice;
    }

    public ConversationElement(List<List<string>> _branchVariables, List<List<bool>> _branchConditions, List<int> _branchResults, int _next= -1) {
        ///<summary>
        ///Create a Branch Element
        ///</summary>
        branchConditions = _branchConditions;
        branchVariables = _branchVariables;
        branchResults = _branchResults;
        next = _next;
        type = ConversationElementType.Branch;
    }

    public ConversationElement() {
        ///<summary>
        ///Create an End Element
        ///</summary>
        type = ConversationElementType.End;
    }
}
