using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour, IInteractable
{
    public Item item;
    public string itemName;
    public float weight;
    public Sprite InventoryIcon;

    void Start()
    {
        gameObject.tag = "Interactable";
        
    }
    
    public void Action(PlayerManager player)
    {
        if (player.AddItem(CreateItem()))
        {

            gameObject.SetActive(false);
            Debug.Log("de item word toegevoegd");
            InventoryUi.instance.AddUIItem(this);
        }
    }
    public void Respawn()
    {
        //respawn object
        //bij speler
        gameObject.SetActive(true);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward;
    }

    public abstract Item CreateItem();
}
