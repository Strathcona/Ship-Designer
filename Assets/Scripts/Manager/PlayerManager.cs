using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour, IInitialized
{
    public List<Magnate> allPlayers = new List<Magnate>();
    public Magnate activePlayer;
    public static PlayerManager instance;

    public void Initialize() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another Player Manager somewhere");
        }
        activePlayer = new Magnate();
    }
}
