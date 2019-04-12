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
    public GeneralDisplayList speciesList;
    public Button nextButton;
    public Magnate player;

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
        speciesList.Display(GameDataManager.instance.Species);
        speciesList.OnListElementSelectedEvent += portraitGenerator.SetSpecies;
        firstName.onValueChanged.AddListener(SetPlayerFirstName);
        lastName.onValueChanged.AddListener(SetPlayerLastName);
        firstName.text = StringLoader.GetAString("FirstNamesMasculine");
        lastName.text = StringLoader.GetAString("LastNames");
        player.Portrait = portraitGenerator.Portrait;
    }

    public void SetPlayerFirstName(string input) {
        player.FirstName = input;
    }

    public void SetPlayerLastName(string input) {
        player.LastName = input;
    }
}
