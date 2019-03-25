using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class ChatWindow : MonoBehaviour, IConversationReader 
{
    public TextScroll chatMessages;
    public GameObject choiceButtonParent;
    public GameObject choiceButtonPrefab;
    public ConversationTree conversationTree;
    public List<GameObject> choiceButtons = new List<GameObject>();
    public float timer = 0.0f;
    public float timeTillResponse = 1.0f;
    public Action OnConversationFinish;

    public void PlayConversationTree(ConversationTree tree, Action onConversationFinish) {
        OnConversationFinish = onConversationFinish;
        Clear();
        conversationTree = ConversationTreeLoader.GetTree("GenericFirstPartSupply");
        conversationTree.currentReader = this;
        conversationTree.StartTree(conversationTree.elements[0], this);
    }

    public void Update() {
        timer += Time.deltaTime;
        if(timer > timeTillResponse) {
            if (!conversationTree.waitingOnChoice) {
                conversationTree.NextNode();
                timer = 0;
            }
        }
    }


    public void DisplayText(string text) {
        chatMessages.DisplayMessage(text, true);
    }

    public void DisplayText(string text, bool left) {
        chatMessages.DisplayMessage(text, left);
    }


    public void DisplayChoice(List<string> choices, System.Action<int> GiveChoiceResponse) {
        int neededButtons = choices.Count - choiceButtons.Count;
        if(neededButtons > 0) {
            for(int j = 0; j < neededButtons; j++) {
                GameObject g = Instantiate(choiceButtonPrefab) as GameObject;
                g.transform.SetParent(choiceButtonParent.transform);
                g.SetActive(false);
                choiceButtons.Add(g);
            }
        }
        for(int i = 0; i < choices.Count; i++) {
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].GetComponentInChildren<Text>().text = choices[i];
            var pass = i;
            var pass2 = choices[i];
            Button b = choiceButtons[i].GetComponent<Button>();
            b.onClick.AddListener(delegate { GiveChoiceResponse(pass); });
            b.onClick.AddListener(delegate { DisplayText(pass2, false); });
            b.onClick.AddListener(HideChoices);
        }
    }

    public void HideChoices() {
        foreach(GameObject g in choiceButtons) {
            g.GetComponent<Button>().onClick.RemoveAllListeners();
            g.SetActive(false);
        }
    }
    
    public string KeywordReplace(string text) {
        return text;
    }
    public void Clear() {
        conversationTree = null;
        chatMessages.Clear();
    }

    public void EndConversation() {
        OnConversationFinish();
    }
}
