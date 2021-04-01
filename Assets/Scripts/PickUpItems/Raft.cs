using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : Pickup
{
    // Start is called before the first frame update
    public override Item CreateItem()
    {
        return new RaftItem(itemName, weight);
    }
}
