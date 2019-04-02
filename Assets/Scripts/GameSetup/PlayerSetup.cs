using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;
using System;

public class PlayerSetup : MonoBehaviour{
    public InputField firstName;
    public InputField lastName;
    public Dropdown title;
    public PortraitGenerator portraitGenerator;
    public Button nextButton;
    public HumanPlayer player;

    private void Awake() {
        player = PlayerManager.instance.activePlayer;
        player.Funds += 2000;
        title.ClearOptions();
        title.options.Add(new Dropdown.OptionData("<i>None</i>"));
        foreach (string s in StringLoader.GetAllStrings("Titles")) {
            title.options.Add(new Dropdown.OptionData(s));
        }
        title.value = 0;
        title.RefreshShownValue();
    }

    private void Start() {
        firstName.onValueChanged.AddListener(SetPlayerFirstName);
        lastName.onValueChanged.AddListener(SetPlayerLastName);
        firstName.text = StringLoader.GetAString("FirstNamesMasculine");
        lastName.text = StringLoader.GetAString("LastNames");
        player.Portrait = portraitGenerator.Portrait;
        nextButton.interactable = false;
    }

    public void SetPlayerFirstName(string input) {
        player.FirstName = input;
        if (input != "") {
            nextButton.interactable = true;
        } else {
            nextButton.interactable = false;
        }
    }

    public void SetPlayerLastName(string input) {
        player.LastName = input;
        if (input != "") {
            nextButton.interactable = true;
        } else {
            nextButton.interactable = false;
        }
    }
}
