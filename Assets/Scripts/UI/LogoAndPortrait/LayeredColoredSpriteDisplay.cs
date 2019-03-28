using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LayeredColoredSpriteDisplay: MonoBehaviour {

    public List<Image> layers;
    public GameObject imagePrefab;
    public GameObject root;
    private GameObjectPool imagePool;
    public LayeredColoredSprite layeredColoredSprite;

    private void Awake() {
        imagePool = new GameObjectPool(imagePrefab, root);
    }

    public void DisplayLogo(LayeredColoredSprite layeredColoredSprite) {
        imagePool.ReleaseAll();
        if (layeredColoredSprite != null) {
            this.layeredColoredSprite.OnIHasLayeredSpriteChangeEvent -= RefreshDisplay;
            this.layeredColoredSprite = layeredColoredSprite;
            RefreshDisplay(this.layeredColoredSprite);
            this.layeredColoredSprite.OnIHasLayeredSpriteChangeEvent += RefreshDisplay;
        }
    }

    public void RefreshDisplay(IHasLayeredSprites layeredColoredSprite) {
        for (int i = 0; i < layeredColoredSprite.Sprites.Length; i++) {
            Image image = imagePool.GetGameObject().GetComponent<Image>();
            image.sprite = layeredColoredSprite.Sprites[i];
            image.color = layeredColoredSprite.Colors[i];
        }
    }
}
