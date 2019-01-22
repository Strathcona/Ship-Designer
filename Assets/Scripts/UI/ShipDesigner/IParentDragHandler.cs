using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParentDragHandler {
    void ChildDragStart(GameObject started);
    void ChildDrag();
    void ChildDragEnd();
    void ChildPointerEnter(GameObject entered);
    void ChildPointerExit(GameObject exited);
}
