using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData item;

    void Pickup()
    {
       InventoryManager.Instance.Add(item);
       Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Pickup();
    }
}
