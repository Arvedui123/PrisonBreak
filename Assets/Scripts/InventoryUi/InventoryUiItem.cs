using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUiItem : MonoBehaviour
{
    public Image itemimage;
    public TextMeshProUGUI itemName;
    private Pickup pickup;

    public void RegisterPickup(string name, Sprite icon)
    {
        itemimage.sprite = icon;
        itemName.text = name;
        this.name = name;
    }
    public void HandleClick()
    {
        Debug.Log("HandleClick" + name);
        InventoryUi.instance.RemoveUIItem(itemName.text);
        Destroy(gameObject);
    }
}
