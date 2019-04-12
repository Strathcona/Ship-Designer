using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public GeneralDisplay display;

    private void Start() {
        if (display == null) {
            display = GetComponent<GeneralDisplay>();
            if (display == null) {
                Debug.LogError("Player Display couldn't find GeneralDisplay on " + gameObject.name);
            }
        }
        PlayerManager.instance.activePlayer.OnPlayerDetailsChangeEvent += DisplayPlayer;
    }

    public void DisplayPlayer(Person player) {
        display.Display(player);
    }

}
