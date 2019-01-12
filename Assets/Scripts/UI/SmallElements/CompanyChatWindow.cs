using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyChatWindow : MonoBehaviour, IConversationReader 
{
    public Company company;
    public PartOrder partOrder;
    public TextScroll companyChatMessages;
    public SmallCompanyDisplay companyChatDisplay;
    public GameObject choiceButtonParent;
    public GameObject choiceButtonPrefab;
    public ConversationTree conversationTree;
    public List<GameObject> choiceButtons = new List<GameObject>();
    public float timer = 0.0f;
    public float timeTillResponse = 1.0f;
    private static Dictionary<string, string> keywordReplacements;
    public Action<Company, PartOrder, bool> OnConversationFinish;

    public void StartChatWith(Company c, PartOrder po, Action<Company, PartOrder, bool> onConversationFinish) {
        OnConversationFinish = onConversationFinish;
        Clear();
        company = c;
        partOrder = po;
        keywordReplacements = new Dictionary<string, string>() {
            {"*COMPANY*", company.name },
            {"*PRICE*",  po.price.ToString()+" credits"},
            {"*TIME*",  po.time.ToString()+" ticks"}
        };
        if(partOrder.units == 1) {
            keywordReplacements.Add("*UNITS*", po.units.ToString() + " unit");
        } else {
            keywordReplacements.Add("*UNITS*", po.units.ToString() + " units");
        }
        conversationTree = ConversationTree.GetTestTree();
        conversationTree.currentReader = this;
        companyChatDisplay.DisplayCompany(company);
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
        companyChatMessages.DisplayMessage(text, true);
    }

    public void DisplayText(string text, bool left) {
        companyChatMessages.DisplayMessage(text, left);
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
        foreach(string key in keywordReplacements.Keys) {
            text = text.Replace(key, keywordReplacements[key]);
        }
        return text;
    }
    public void Clear() {
        conversationTree = null;
        companyChatMessages.Clear();
    }

    public void EndConversation() {
        if (conversationTree.conversationVariables.ContainsKey("Agree")) {
            CloseChatWindow(conversationTree.conversationVariables["Agree"]);
        } else {
            CloseChatWindow(false);
        }
    }

    public bool CloseChatWindow(bool agree) {
        OnConversationFinish(company, partOrder, agree);
        return agree;
    }
}
