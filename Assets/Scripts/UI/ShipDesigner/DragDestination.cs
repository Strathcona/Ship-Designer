using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDestination : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public IParentDragHandler handler;

    public void OnPointerEnter(PointerEventData eventData) {
        handler.ChildPointerEnter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData) {
        handler.ChildPointerExit(gameObject);
    }
}
