using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour{
    public InputField playerNameInput;
    public Button nextButton;
    public HumanPlayer player;

    private void Start() {
        player = PlayerManager.instance.activePlayer;
        player.ChangeFunds(2000);
        playerNameInput.onValueChanged.AddListener(SetPlayerName);
        nextButton.interactable = false;
    }

    public void SetPlayerName(string input) {
        player.FirstName = input;
        if (input != "") {
            nextButton.interactable = true;
        } else {
            nextButton.interactable = false;
        }
    }
}
