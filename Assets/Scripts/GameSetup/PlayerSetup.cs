using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour{
    public InputField playerNameInput;
    public Button nextButton;
    public HumanPlayer player;

    private void Start() {
        player = new HumanPlayer();
        playerNameInput.onValueChanged.AddListener(SetPlayerName);
        nextButton.interactable = false;
    }

    public void FinalizePlayer() {
        PlayerManager.instance.SetActivePlayer(player);
    }

    public void SetPlayerName(string input) {
        player.name = input;
        if (input != "") {
            nextButton.interactable = true;
        } else {
            nextButton.interactable = false;
        }
    }
}
