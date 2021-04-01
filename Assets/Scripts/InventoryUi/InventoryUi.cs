using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public static InventoryUi instance;

    public GameObject Uiitemprefeb;

    private List<Pickup> uiItems;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        uiItems = new List<Pickup>();
    }
    public void AddUIItem(Pickup p)
    {
        if (uiItems.Contains(p) == false)
        {
            Debug.Log("it sees the item");
            GameObject t = Instantiate(Uiitemprefeb, transform);
            InventoryUiItem i = t.GetComponent<InventoryUiItem>();
            if (i != null)
            {
                Debug.Log(p);
                i.RegisterPickup(p.itemName, p.InventoryIcon);
                uiItems.Add(p);
            }
        }
    }
    public void RemoveUIItem(string name)
    {
        for (int i = 0; i < uiItems.Count; i++)
        {
            Pickup p = uiItems[i];
            if (p.itemName == name)
            {
                //deze moeten we verwijderen
                uiItems.Remove(p);
                Debug.Log("item removed");
                p.Respawn();
                Debug.Log("item deployed");
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
