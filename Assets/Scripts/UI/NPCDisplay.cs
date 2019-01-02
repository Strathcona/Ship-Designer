using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDisplay : MonoBehaviour {
    public NPC npc;
    public Image npcPortrait;
    public Text npcName;

    public void DisplayNPC(NPC n) {
        Clear();
        npc = n;
        npcName.text = n.firstName + " " + n.lastName;
    }

    public void Clear() {
        npc = null;
        npcName.text = "";
    }
}
