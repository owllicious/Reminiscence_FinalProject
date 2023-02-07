using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour 
{
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictonary; //A list that has a key pretty much, its a little diffuclt to explain
    public List<InventoryItem> inventory { get; private set; }  //list of items

    private List<InventoryItem> inventoryChanged;

    public delegate void Changed();
    public event Changed OnInventoryChangedEvent;
    private void Awake()
    {
        inventory = new List<InventoryItem>(); //generate the list
        m_itemDictonary = new Dictionary<InventoryItemData, InventoryItem>(); //generate the dictionary
        inventoryChanged = inventory;
    }

    public InventoryItem Get(InventoryItemData referenceData) //gets the item
    {
        if (m_itemDictonary.TryGetValue(referenceData, out InventoryItem value)) 
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData referenceData) //adds the item to the inventory
    {
        if(m_itemDictonary.TryGetValue(referenceData, out InventoryItem value)) //if the same item already exists it creates a stack
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData); //if the item doesnt exist it adds a new item
            inventory.Add(newItem);
            m_itemDictonary.Add(referenceData, newItem);
        }
    }
    public void Remove(InventoryItemData referenceData) //removes an item from the inventory
    {
        if (m_itemDictonary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack(); //remove 1 from the stack

            if (value.stackSize == 0) //if there are none of the item left remove from inventory
            {
                inventory.Remove(value);
                m_itemDictonary.Remove(referenceData);
            }
        }
    }
    public void Update()
    {
        if (inventoryChanged != inventory)
        {
            inventoryChanged = inventory;
            OnInventoryChangedEvent();
        }
    }

}
