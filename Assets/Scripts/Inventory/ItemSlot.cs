using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;

    [SerializeField] private TextMeshProUGUI itemLabel;

    public void Set(InventoryItem item)
    {
        itemIcon.sprite = item.data.icon;
        itemLabel.text = item.data.displayName;
    }
}
