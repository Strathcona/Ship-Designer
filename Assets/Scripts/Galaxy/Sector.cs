using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameConstructs;

public class Sector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    private GalaxyEntity owner;
    public GalaxyEntity Owner {
        get { return owner; }
        set { owner = value;
            Refresh();
        }
    }
    public Coord coord;
    public string name = "";
    public List<int> claimedIDs = new List<int>();
    public Color baseColor;
    public int systemCount = 0;
    public Image backgroundImage;
    public Image foregroundImage;
    public Color foregroundColorHover = Color.yellow;
    public Color foregroundColorSelect = Color.green;
    public Image selectionOutline;
    public Sector[] neighbours =  new Sector[8];
    //arranged
    // 0, 1, 2
    // 3, X, 4
    // 5, 6, 7
    //may be null if there's nothing there on the edge
    public static int[] orthogonal = new int[4] { 1, 3, 4, 6 };
    public static Coord[] neighbourDeltas = new Coord[8] {
        new Coord(1, -1),
        new Coord(1, 0),
        new Coord(1, 1),
        new Coord(0, -1),
        new Coord(0, 1),
        new Coord(-1, -1),
        new Coord(-1, 0),
        new Coord(-1, 1),
    };
    public GalaxyMap map;
    public bool selected;
    public List<GalaxyFeature> features = new List<GalaxyFeature>();

    public void Refresh() {

    }

    public void AddGalaxyFeature(GalaxyFeature g) {
        features.Add(g);
    }

    public void ShowFeatures(GalaxyFeatureType t) {
        List<GalaxyFeature> f = features.FindAll(i => i.featureType == t);
        if(f.Count != 0) {
            Debug.Log("Feature Found!");
            foregroundImage.gameObject.SetActive(true);
            foregroundImage.sprite = f[0].icon;
            foregroundImage.color = f[0].iconColor;
        } else {
            foregroundImage.gameObject.SetActive(false);
        }
    }

    public void ShowOwnerColor() {
        if (owner != null) {
            backgroundImage.color = owner.color;
        } else {
            backgroundImage.color = baseColor;
        }
    }

    public void SetHover(bool on) {
        if (on) {
            selectionOutline.gameObject.SetActive(true);
            selectionOutline.color = foregroundColorHover;
        } else if(!selected){
            selectionOutline.gameObject.SetActive(false);
        }
        if(!on && selected) {
            selectionOutline.gameObject.SetActive(true);
            selectionOutline.color = foregroundColorSelect;
        }
    }

    public void SetSelection(bool on) {
        if (on) {
            selected = true;
            selectionOutline.gameObject.SetActive(true);
            selectionOutline.color = foregroundColorSelect;
        } else {
            selectionOutline.gameObject.SetActive(false);
            selected = false;
        }
    }

    public void ShowBaseColor() {
        backgroundImage.color = baseColor;
    }

    public void OnPointerEnter(PointerEventData data) {
        map.TilePointerEnter(this);
    }

    public void OnPointerExit(PointerEventData data) {
        map.TilePointerExit(this);
    }

    public void Clear() {
        Owner = null;
        systemCount = 0;
    }
}
