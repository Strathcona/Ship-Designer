using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour{
    public InputField playerNameInput;
    public PortraitGenerator portraitGenerator;
    public Button nextButton;
    public HumanPlayer player;

    private void Start() {
        player = PlayerManager.instance.activePlayer;
        player.Funds += 2000;
        playerNameInput.onValueChanged.AddListener(SetPlayerName);
        nextButton.interactable = false;
    }

    public void SetPlayerName(string input) {
        player.FirstName = input;
        player.Portrait = portraitGenerator.Portrait;
        if (input != "") {
            nextButton.interactable = true;
        } else {
            nextButton.interactable = false;
        }
    }
}
