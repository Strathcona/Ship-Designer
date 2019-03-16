using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PartSelector : MonoBehaviour, IParentDragHandler {
    public Ship ship;
    public Button finishedEditingButton;

    private GameObjectPool partIconPool;
    public GameObject partIconPrefab;
    public GameObject partIconRoot;

    private GameObjectPool hardpointDisplayPool;
    public GameObject hardpointDisplayPrefab;
    public GameObject hardpointDisplayRoot;

    public GameObject overTop;
    public GameObject dragClone;


    private void Awake() {
        hardpointDisplayPool = new GameObjectPool(hardpointDisplayPrefab, hardpointDisplayRoot, AttachDragDestination);
        partIconPool = new GameObjectPool(partIconPrefab, partIconRoot, AttachDragSource);
    }

    public void AttachDragSource(GameObject g) {
        g.AddComponent<DragSource>();
        g.GetComponent<DragSource>().handler = this;
    }

    public void AttachDragDestination(GameObject g) {
        g.AddComponent<DragDestination>();
        g.GetComponent<DragDestination>().handler = this;
    }

    public void LoadShip(Ship s) {
        Clear();
        ship = s;
        foreach (Part p in DesignManager.instance.GetAllParts()) {
            GameObject g = partIconPool.GetGameObject();
            PartIcon pi = g.GetComponent<PartIcon>();
            pi.DisplayPart(p);
        }
        foreach(Hardpoint h in ship.hardpoints) {
            GameObject g = hardpointDisplayPool.GetGameObject();
            HardpointDisplay ha = g.GetComponent<HardpointDisplay>();

            DragDestination dd = g.GetComponent<DragDestination>();
            if (dd == null) {
                g.AddComponent<DragDestination>();
                g.GetComponent<DragDestination>().handler = this;
            }

            ha.DisplayHardpoint(h);
        }
    }


    public void Clear() {
        ship = null;
        hardpointDisplayPool.ReleaseAll();
        partIconPool.ReleaseAll();
    }

    public void ChildDragStart(GameObject start) {
        dragClone = Instantiate(start, transform);
        dragClone.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
        //don't want our cloned drag object also triggering drag events and the like
        dragClone.GetComponent<Image>().raycastTarget = false;
        PartIcon dragIcon = dragClone.GetComponent<PartIcon>();
        dragIcon.DisplayPart(start.GetComponent<PartIcon>().part);

    }

    public void ChildDrag() {
        dragClone.transform.position = Input.mousePosition;
    }

    public void ChildDragEnd() {
        if(overTop != null) {
            Debug.Log("Drag ended over top of" + overTop.name);
            PartIcon pi = dragClone.GetComponent<PartIcon>();
            HardpointDisplay hd = overTop.GetComponent<HardpointDisplay>();
            if(hd != null) {
                if (hd.hardpoint.MountPart(pi.part)) {
                    Debug.Log("Mounted!");
                    hd.Refresh();
                }
            }
        }
        Destroy(dragClone);
    }

    public void ChildPointerEnter(GameObject entered) {
        overTop = entered;
    }

    public void ChildPointerExit(GameObject exited) { 
        if(overTop == exited) {
            overTop = null;
        }
    }

}
