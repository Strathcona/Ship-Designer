using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameConstructs;
using System;

public class SectorObject: MonoBehaviour{

    public int bitmask;
    public Color baseColor;
    public SpriteRenderer bottom;
    public SpriteRenderer middle;
    public SpriteRenderer top;
    public SectorData sectorData;
    public int SortingOrder {
        set { bottom.sortingOrder = value;
            middle.sortingOrder = value;
            top.sortingOrder = value;
        }
    }
    public void DisplaySector(SectorData _sectorData) {
        sectorData = _sectorData;
        sectorData.onRefresh.Add(Refresh);
    }

    public void Refresh() {
    }

    public void SetColor(Color c) {
        bottom.color = c;
        middle.color = c;
        top.color = c;
    }
/*
    public void RecalculateBorder() {
        if (sectorData.Owner != null) {
            borderImage.gameObject.SetActive(true);
            bitmask = 0;
            //X, 1, X
            //2, O, 4,
            //X, 8, X
            if (sectorData.neighbours[1] != null) {
                if (sectorData.neighbours[1].Owner != sectorData.Owner) {
                    bitmask += 1;
                }
            } else {
                bitmask += 1;
            }

            if (sectorData.neighbours[3] != null) {
                if (sectorData.neighbours[3].Owner != sectorData.Owner) {
                    bitmask += 2;
                }
            } else {
                bitmask += 2;
            }
            if (sectorData.neighbours[4] != null) {
                if (sectorData.neighbours[4].Owner != sectorData.Owner) {
                    bitmask += 4;
                }
            } else {
                bitmask += 4;
            }
            if (sectorData.neighbours[6] != null) {
                if (sectorData.neighbours[6].Owner != sectorData.Owner) {
                    bitmask += 8;
                }
            } else {
                bitmask += 8;
            }
            switch (bitmask) {
                case 0:
                    borderImage.gameObject.SetActive(false);
                    break;
                case 1:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 2:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 3:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 4:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270.0f));
                    break;
                case 5:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270.0f));
                    break;
                case 6:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopBottom"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 7:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 8:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["Top"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180.0f));
                    break;
                case 9:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopBottom"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 10:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 11:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f));
                    break;
                case 12:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeft"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180.0f));
                    break;
                case 13:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270.0f));
                    break;
                case 14:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRight"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180.0f));
                    break;
                case 15:
                    borderImage.sprite = SpriteLoader.bitmaskBorderSprites["TopLeftRightBottom"];
                    borderImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
            }
        } else {
            borderImage.gameObject.SetActive(false);
        }
    }
    */
    public void Clear() {
        sectorData = null;
    }
}
