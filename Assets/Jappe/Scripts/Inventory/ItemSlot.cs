using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemSlot : MonoBehaviour, IDropHandler {

    public GameObject Item {
        get {
            return transform.childCount > 0
                    ? transform.GetChild(0).gameObject
                    : null;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        if (!Item) {
            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);
        }
    }
}