using UnityEngine;
using System.Collections;
using GameConstructs;

public struct GalaxyFeature {
    public Sprite icon;
    public Color iconColor;
    public string name;
    public GalaxyFeatureType featureType;

    public GalaxyFeature(string _name, GalaxyFeatureType _featureType, Color _iconColor) {
        name = _name;
        featureType = _featureType;
        iconColor = new Color(_iconColor.r * 0.5f, _iconColor.g * 0.5f, _iconColor.b * 0.5f);
        icon = SpriteLoader.GetFeatureSprite(featureType.ToString());
    }
}
