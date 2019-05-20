using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameConstructs;
using System;

public class SectorObject: MonoBehaviour, IDisplayed{

    public int bitmask;
    public Color baseColor;
    private Color borderColor;
    public Color BorderColor {
        get { return borderColor; }
        set {
            borderColor = value;
            border.color = borderColor;
        }
    }
    public SpriteRenderer background;
    public SpriteRenderer border;
    public Sprite currentBorder;
    public SectorData sectorData;
    public string[] DisplayStrings {
        get { return sectorData.DisplayStrings; }
    }
    public LayeredColoredSprite[] DisplaySprites {
        get { return sectorData.DisplaySprites; }
    }
    public event Action<IDisplayed> DisplayUpdateEvent;
    public void DisplaySector(SectorData _sectorData) {
        sectorData = _sectorData;
        sectorData.SectorDataRefreshEvent += Refresh;
        Refresh(sectorData);
    }

    public void Refresh(SectorData data) {
        if(sectorData.Owner != null) {
            BorderColor = sectorData.Owner.flag.Colors[1];
        }
        RecalculateBorder();
    }

    public void SetTransparent(bool transparent) {
        background.enabled = !transparent;
    }

    public void SetColor(Color c) {
        background.color = c;
    }

    public void DisplayBaseColor() {
        background.color = baseColor;
    }

    public void DisplayOwner() {
        if(sectorData.Owner != null) {
            SetColor(sectorData.Owner.flag.Colors[0]);
        }
    }

    public void RecalculateBorder() {
        if (sectorData.Owner != null) {
            border.enabled = true;
            bitmask = 0;
            //X, 1, X
            //2, O, 4,
            //X, 8, X
            //neighbours
            //2, 4, 7
            //1, O, 6,
            //0, 3, 5
            if (sectorData.neighbours[4] == null) {
                bitmask += 1;
            } else if (sectorData.neighbours[4].Owner != sectorData.Owner) {
                bitmask += 1;
            }
            if (sectorData.neighbours[1] == null) {
                bitmask += 2;
            } else if (sectorData.neighbours[1].Owner != sectorData.Owner) {
                bitmask += 2;
            }
            if (sectorData.neighbours[6] == null) {
                bitmask += 4;
            } else if (sectorData.neighbours[6].Owner != sectorData.Owner) {
                bitmask += 4;
            }
            if (sectorData.neighbours[3] == null) {
                bitmask += 8;
            } else if (sectorData.neighbours[3].Owner != sectorData.Owner) {
                bitmask += 8;
            }
            switch (bitmask) {
                case 0:
                    border.enabled = false;
                    break;
                case 1:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 2:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 3:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 4:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270.0f));
                    break;
                case 5:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270.0f));
                    break;
                case 6:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopBottom"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 7:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 8:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180.0f));
                    break;
                case 9:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopBottom"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 10:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 11:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 12:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180.0f));
                    break;
                case 13:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270.0f));
                    break;
                case 14:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180.0f));
                    break;
                case 15:
                    border.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRightBottom"];
                    border.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
            }
            currentBorder = border.sprite;
        } else {
            border.enabled = false;
        }
    }
    public void Clear() {
        sectorData = null;
    }
}
