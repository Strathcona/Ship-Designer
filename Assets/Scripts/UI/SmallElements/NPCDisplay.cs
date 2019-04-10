using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDisplay : MonoBehaviour {
    public NPC npc;
    public LayeredColoredSpriteDisplay npcPortrait;
    public Text npcName;

    public void DisplayNPC(NPC n) {
        Clear();
        npc = n;
        npcName.text = n.FirstName + " " + n.LastName;
        npcPortrait.DisplayLayeredColoredSprite(npc.Portrait);
    }

    public void Clear() {
        npc = null;
        npcName.text = "";
    }
}
