using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private InventorySystem inventorySystem;

    private GameObject _managers;
    [SerializeField] private GameObject itemPrefab;
    
    public void Start()
    {
        _managers = GameObject.Find("-GameManagers-");

        inventorySystem = _managers.GetComponent<InventorySystem>();
        inventorySystem.OnInventoryChangedEvent += OnUpdateInventory;
    }
    private void OnUpdateInventory()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    private void DrawInventory()
    {
        foreach(InventoryItem item in inventorySystem.inventory)
        {
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(itemPrefab);
        obj.transform.SetParent(transform,false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
