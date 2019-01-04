using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SpriteLoader {

    private static Object[] npcImages;

    static SpriteLoader() {
        npcImages = Resources.LoadAll("Images/NPCs", typeof(Sprite));
    }

    public static Sprite GetNPCImage() {
        return (Sprite)npcImages[Random.Range(0, npcImages.Length)];
    }
}
