using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour, IInitialized
{
    public List<Player> allPlayers = new List<Player>();
    public HumanPlayer activePlayer;
    public event Action<HumanPlayer> ActivePlayerChangedEvent;
    public static PlayerManager instance;

    public void Initialize() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another Player Manager somewhere");
        }
    }

    public void SetActivePlayer(HumanPlayer humanPlayer) {
        activePlayer = humanPlayer;
        ActivePlayerChangedEvent?.Invoke(activePlayer);
    }
}
