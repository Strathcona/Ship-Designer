using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartImageDisplay : MonoBehaviour
{
    public Image image;
    public Part part;
    private void Awake() {
        if (image == null) {
            image = GetComponent<Image>();
            if (image == null) {
                Debug.LogError("Part Image Display couldn't find Image on " + gameObject.name);
            }
        }
    }

    public void DisplayPart(Part part) {
        if (part != null) {
            part.OnManufactuerChange -= UpdateDisplay;
        }
        this.part = part;
        if (part != null) {
            if(part.sprite != null) {
                gameObject.SetActive(true);
                image.sprite = part.sprite;
            } else {
                gameObject.SetActive(false);
            }
            part.OnManufactuerChange += UpdateDisplay;
        }
    }

    public void UpdateDisplay() {
        if (part != null) {
            if (part.sprite != null) {
                gameObject.SetActive(true);
                image.sprite = part.sprite;
            } else {
                gameObject.SetActive(false);
            }
         }
    }
}
