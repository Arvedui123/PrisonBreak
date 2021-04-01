using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Inventory
{
    public List<Item> items;
    private float weight;
    private float maximumWeight;
    public bool escape = false;

    public Inventory()
    {
        items = new List<Item>();
        weight = 0;
        maximumWeight = 200;
    }

    public Inventory(float maximumWeight) : this()
    {
        this.maximumWeight = maximumWeight;
    }

    public bool SetMaximumWeight(float maxWeight)
    {
        if (maxWeight >= weight)
        {
            maximumWeight = maxWeight;
            return true;
        }
 
        return false;
    }
    
    public bool AddItem(Item i)
    {
        if (weight + i.GetWeight() <= maximumWeight)
        {
            items.Add(i);
            weight += i.GetWeight();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveItem(Item i)
    {
        bool success = items.Remove(i);

        if (success)
        {
            weight -= i.GetWeight();
        }

        return success;
    }

    public bool Parts()
    {
        int raftParts = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is RaftItem)
            {
                raftParts += 1;
            }
        }
        if (raftParts > 4)
        {
            Debug.Log("YOU HAVE:" + raftParts);
            escape = true;
            return true;
        }
        else
        {
            Debug.Log("YOU HAVE:" + raftParts);
            return false;
        }
    }

    public bool HasItem(Item i)
    {
        return items.Contains(i);
    }

    public bool CanOpenDoor(int id)
    {
        bool result = false;

        foreach (Item item in items)
        {
            if (item is AccessItem)
            {
                if (((AccessItem) item).OpensDoor(id))
                {
                    result = true;
                }
            }
        }

        return result;
    }

    public int Count()
    {
        return items.Count;
    }

    public float GetCurrentWeight()
    {
        return weight;
    }
    
    public void DebugInventory()
    {
        Debug.Log("Inventory has " + Count() + " items");
        Debug.Log("Total weight: " + GetCurrentWeight());

        foreach (Item item in items)
        {
            Debug.Log(item.GetName() + "-----" + item.GetWeight() + "Kg");
        }
    }
}
