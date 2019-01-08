using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyChatWindow : MonoBehaviour, IConversationReader 
{
    public Company company;
    public TextScroll companyChatMessages;
    public SmallCompanyDisplay companyChatDisplay;
    public GameObject choiceButtonParent;
    public GameObject choiceButtonPrefab;
    public ConversationTree conversationTree;
    public List<GameObject> choiceButtons = new List<GameObject>();
    public float timer = 0.0f;
    public float timeTillResponse = 1.0f;


    public void StartChatWith(Company c) {
        Clear();
        company = c;
        conversationTree = ConversationTree.GetTestTree();
        conversationTree.currentReader = this;
        companyChatDisplay.DisplayCompany(company);
        conversationTree.StartTree(conversationTree.elements[0], this);
    }

    public void Update() {
        timer += Time.deltaTime;
        if(timer > timeTillResponse) {
            if (!conversationTree.waitingOnChoice) {
                if(conversationTree.NextNode() == true) {
                    timer = 0;
                }
            }
        }
    }


    public void DisplayText(string text) {
        companyChatMessages.DisplayMessage(text, true);
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
            Button b = choiceButtons[i].GetComponent<Button>();
            b.onClick.AddListener(delegate { GiveChoiceResponse(pass); });
            b.onClick.AddListener(HideChoices);
        }
    }

    public void HideChoices() {
        foreach(GameObject g in choiceButtons) {
            g.GetComponent<Button>().onClick.RemoveAllListeners();
            g.SetActive(false);
        }
    }
    
    public void Clear() {
        conversationTree = null;
        companyChatMessages.Clear();
    }
}
