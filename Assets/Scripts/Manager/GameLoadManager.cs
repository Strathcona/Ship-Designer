using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadManager : MonoBehaviour {

    private void Awake() {
        foreach (Component c in GetComponents(typeof(Component))){
            IInitialized i = c as IInitialized;
            if(i != null) {
                i.Initialize();
            }
        }

        //if (TitleSceneData.newGame) {
        //if (true) {
           ScreenManager.instance.DisplayCanvas("New Game");
        //}
    }

    public void DoneStartingNewGame() {
        ScreenManager.instance.DisplayCanvas("World Galaxy Map UI");
    }
}
