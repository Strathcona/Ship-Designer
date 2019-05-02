using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WindowPositionToggler : MonoBehaviour
{
    public Button toggleButton;
    public Text buttonText;
    public GameObject objectToToggle;
    public Vector2 objectToggledPosition;
    private Vector2 objectOriginalPosition;
    private RectTransform objectRectTransform;
    public enum ToggleDirection { up, down, left, right };
    public ToggleDirection toggleDirection = ToggleDirection.down;
    //what axis it toggles, used for icon image
    public bool toggled = false;
    private Vector2 targetPosition;
    public float speed = 0.1f;

    private void Awake() {
        objectRectTransform = objectToToggle.GetComponent<RectTransform>();
        buttonText = toggleButton.GetComponentInChildren<Text>();
        objectOriginalPosition = objectRectTransform.anchoredPosition;
        toggleButton.onClick.AddListener(TogglePosition);
        UpdateImage();
    }

    public void TogglePosition() {
        if(toggled == true) {
            targetPosition = objectOriginalPosition;
            StartCoroutine(MoveObjectPosition(false));
        } else {
            targetPosition = objectToggledPosition;
            StartCoroutine(MoveObjectPosition(true));
        }
    }

    public IEnumerator MoveObjectPosition(bool toggleStatus) {
        toggleButton.interactable = false;
        while (true) {
            if ((objectRectTransform.anchoredPosition - targetPosition).SqrMagnitude() < 0.1) {
                objectRectTransform.anchoredPosition = targetPosition;
                break;
            }
            objectRectTransform.anchoredPosition = Vector2.Lerp(objectRectTransform.anchoredPosition, targetPosition, speed * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }
        toggled = toggleStatus;
        UpdateImage();
        toggleButton.interactable = true;
    }

    private void UpdateImage() {
        switch(toggleDirection){
            case ToggleDirection.up:
                if (toggled) {
                    transform.rotation = Quaternion.Euler(new Vector3(0,0, 180));
                } else {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0,0));
                }
                break;
            case ToggleDirection.down:
                if (toggled) {
                    transform.rotation = Quaternion.Euler(new Vector3(0,0, 0));
                } else {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0,180));
                }
                break;
            case ToggleDirection.left:
                if (toggled) {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                } else {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                }
                break;
            case ToggleDirection.right:
                if (toggled) {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                } else {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                break;
        }        
    }
}
