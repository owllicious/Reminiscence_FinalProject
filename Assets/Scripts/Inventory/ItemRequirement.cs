using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct ItemRequirement 
{
    public InventoryItemData itemdata;
    public int amount;

    private InventorySystem inventorySystem;

    private GameObject _managers;
    public bool HasRequirement()
    {
        _managers = GameObject.Find("-GameManagers-");

        inventorySystem = _managers.GetComponent<InventorySystem>();
        InventoryItem item = inventorySystem.Get(itemdata);

        if (item == null || item.stackSize < amount) { return false; }
        return true;
    }
    
}
