using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem 
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(InventoryItemData source)
    {
        data = source;
        AddToStack();
    }

    public void AddToStack() //adds a duplication of the item 
    {
        stackSize++;
    }
    public void RemoveFromStack() //removes a duplication of the item 
    {
        stackSize--;
    }
}
