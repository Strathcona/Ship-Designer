using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSource : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    public IParentDragHandler handler;

    public void OnDrag(PointerEventData eventData) {
        handler.ChildDrag();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log(this.name + " Got a begin drag call");
        handler.ChildDragStart(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData) {
        handler.ChildDragEnd();
    }
}
