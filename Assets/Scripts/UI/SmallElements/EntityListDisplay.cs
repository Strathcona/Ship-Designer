using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListDisplay : MonoBehaviour {

    public GameObjectPool displayPool;
    public GameObject entityDisplayPrefab;
    public GameObject root;

    public void Awake() {
        displayPool = new GameObjectPool(entityDisplayPrefab, root);
    }

    public void DisplayEntities(GalaxyEntity[] entities) {
        displayPool.ReleaseAll();
        foreach(GalaxyEntity ge in entities) {
            GameObject g = displayPool.GetGameObject();
            g.GetComponent<GeneralDisplay>().Display(ge);
        }
    }

}
