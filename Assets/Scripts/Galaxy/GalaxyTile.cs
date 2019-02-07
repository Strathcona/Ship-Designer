using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyTile : MonoBehaviour {
    public int ownerID = 0;
    public int starCount = 0;
    public Image backgroundImage;
    public Image foregroundImage;
    public List<GalaxyTile> neighbours;

    public void SetBGColor(Color color) {
        backgroundImage.color = color;
    }

    public void Clear() {
        ownerID = 0;
        starCount = 0;
    }
}
