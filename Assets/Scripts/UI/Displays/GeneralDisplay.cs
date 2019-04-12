using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GeneralDisplay: MonoBehaviour {
    public IDisplayed displayed;
    public Text[] textDisplays;
    public LayeredColoredSpriteDisplay[] spriteDisplays;
    Button selectionButton;
    GameObject selectionBorder;
    public bool selectable;
    private bool selected;
    public bool Selected {
        get { return selected; }
        set {
            if (value == true && displayed != null && selectable) {
                selected = value;
                OnSelect();
            } else {
                selected = value;
                Deselect();
            }
        }
    }
    public event Action<GeneralDisplay> OnSelectEvent;

    private void Awake() {
        textDisplays = GetComponentsInChildren<Text>();
        spriteDisplays = GetComponentsInChildren<LayeredColoredSpriteDisplay>();
        selectionButton = GetComponentInChildren<Button>();
        for(int i= 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).gameObject.name == "Selection Border") {
                selectionBorder = transform.GetChild(i).gameObject;
                selectionBorder.SetActive(false);
            }
        }
        if(selectionButton != null) {
            selectionButton.onClick.AddListener(OnSelect);
        }
    }

    public void OnSelect() {
        if (displayed != null && selectable) {
            if (Selected) {
                Deselect();
            } else {
                selected = true;
                OnSelectEvent?.Invoke(this);
                selectionBorder?.SetActive(true);
            }
        }
    }
    public void Deselect() {
        selected = false;
        selectionBorder?.SetActive(false);
    }

    public void Display(IDisplayed displayed) {
        if(this.displayed != null) {
            this.displayed.DisplayUpdateEvent -= UpdateDisplay;
        }
        this.displayed = displayed;
        SetTexts(this.displayed.DisplayStrings);
        SetSprites(this.displayed.DisplaySprites);
    }

    public void UpdateDisplay(IDisplayed displayed) {
        SetTexts(this.displayed.DisplayStrings);
        SetSprites(this.displayed.DisplaySprites);
    }

    private void SetTexts(string[] texts) {
        int number = Mathf.Min(texts.Length, textDisplays.Length);
        for(int i = 0; i < number; i++) {
            textDisplays[i].text = texts[i];
        }
    }

    private void SetSprites(LayeredColoredSprite[] sprites) {
        int number = Mathf.Min(sprites.Length, spriteDisplays.Length);
        for (int i = 0; i < number; i++) {
            spriteDisplays[i].DisplayLayeredColoredSprite(sprites[i]);
        }
    }
}
