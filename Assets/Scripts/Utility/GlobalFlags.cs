using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GlobalFlags : MonoBehaviour{
    public static GlobalFlags instance;

    //if a text field is focused and text entry is happening
    public static bool fieldFocused;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another Global Flags somewhere...");
        }
    }
}
