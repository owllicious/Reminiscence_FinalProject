using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory Item Data")] //When you right click you will now be able to create it from the menu like a new material
public class InventoryItemData : ScriptableObject  //An object template for out items
{
    public string id; //InventoryItem_X <- Enter the ID as such but swap the X with whatever the Item is

    public string displayName; //The Item name

    public Sprite icon; //UI Icon that will be displayed when its added to the inventory

    public GameObject prefab; //The actual game object

}
