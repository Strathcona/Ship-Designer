using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour {
    public Text text;

    private void Start() {
        if (text == null) {
            text = GetComponent<Text>();
            if (text == null) {
                Debug.LogError("Player Name Display couldn't find Text on " + gameObject.name);
            }
        }
        PlayerManager.instance.ActivePlayerChangedEvent += UpdatePlayerName;
    }
    
    public void UpdatePlayerName(HumanPlayer player) {
        text.text = player.name;
    }
    
}
