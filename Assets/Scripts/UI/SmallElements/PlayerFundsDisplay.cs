using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFundsDisplay : MonoBehaviour {
    public Text text;

    private void Start() {
        if (text == null) {
            text = GetComponent<Text>();
            if (text == null) {
                Debug.LogError("Player Name Display couldn't find Text on " + gameObject.name);
            }
        }
        PlayerManager.instance.activePlayer.OnFundsChangeEvent += UpdatePlayerFunds;
        UpdatePlayerFunds(PlayerManager.instance.activePlayer.Funds);
    }

    public void UpdatePlayerFunds(int funds) {
        text.text = funds.ToString();
    }
}
