using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   
    public static InventoryManager Instance;
    public List<ItemData> items = new List<ItemData>();
    
    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemData item)
    {
        items.Add(item);
    }

    public void Remove(ItemData item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        
    }
}
