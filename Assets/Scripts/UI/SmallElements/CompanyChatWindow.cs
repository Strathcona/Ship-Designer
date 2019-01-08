using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyChatWindow : MonoBehaviour
{
    public Company company;
    public TextScroll companyChatMessages;
    public SmallCompanyDisplay companyChatDisplay;
    public List<GameObject> choiceButtons = new List<GameObject>();

    public void StartConversationTree(Company c, ConversationTree t) {

    }
}
