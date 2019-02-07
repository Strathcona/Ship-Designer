using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GalaxyTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    private GalaxyEntity owner;
    public GalaxyEntity Owner {
        get { return owner; }
        set { owner = value;
            Refresh();
        }
    }
    public Coord coord;
    public List<int> claimedIDs = new List<int>();
    public Color baseColor;
    public int systemCount = 0;
    public Image backgroundImage;
    public Image foregroundImage;
    public GameObject selectionOutline;
    public GalaxyTile[] neighbours;
    public GalaxyMap map;

    public void Refresh() {

    }

    public void ShowOwnerColor() {
        if (owner != null) {
            backgroundImage.color = owner.color;
        } else {
            backgroundImage.color = Color.black;
        }
    }

    public void ShowBaseColor() {
        backgroundImage.color = baseColor;
    }

    public void OnPointerEnter(PointerEventData data) {
        map.TilePointerEnter(this);
        selectionOutline.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data) {
        selectionOutline.SetActive(false);
    }

    public void Clear() {
        Owner = null;
        systemCount = 0;
    }
}
