using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour {

    private void Awake() {
        foreach (Component c in GetComponents(typeof(Component))){
            IInitialized i = c as IInitialized;
            if(i != null) {
                i.Initialize();
            }
        }

        //if (TitleSceneData.newGame) {
        if (true) { 

        }
    }
}
