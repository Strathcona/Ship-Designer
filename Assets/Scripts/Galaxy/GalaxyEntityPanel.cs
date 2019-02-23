using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyEntityPanel : MonoBehaviour {
    public Text entityName;
    public Text entityType;
    public NPCDisplay npcDisplay;
    public Text detailText;
    public Button viewButton;
    public GalaxyEntity galaxyEntity;

    public void DisplayEntity(GalaxyEntity g) {
        galaxyEntity = g;
        entityName.text = galaxyEntity.entityName;
        entityType.text = galaxyEntity.governmentName;
        npcDisplay.DisplayNPC(galaxyEntity.leader);
        detailText.text = galaxyEntity.GetDetailString();
    }

    public void ViewEntityScreen() {
        if(galaxyEntity != null) {
            EntityScreen.instance.DisplayEntity(galaxyEntity);
        }
    }

}
