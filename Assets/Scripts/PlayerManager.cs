using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour 
{
    private Inventory inventory;
    public APIConnection aC;
    public Inventory Inv;
    public int raftparts;
    public float initialMaxWeight=100;
    public Camera playerCam;
    private bool dogcheck;
    
    
    void Start()
    {
        raftparts = 0;
        dogcheck = aC.dog;
        Debug.Log("dog is = " + aC.dog);
        inventory = new Inventory(initialMaxWeight);
       // dogcheck = APIConnection.instance.dog;
        //if(dogcheck = false)
      //  {
       //     Debug.Log("script reads fox");
       // }
       // else
        //{
         //   Debug.Log("script reads dog");
       // }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("interacting...");
            Debug.Log(aC.dog);
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider.tag == "Fox" && aC.dog == false)
                {
                    Destroy(GameObject.FindWithTag("PuzzleDoor"));
                }

                if (hit.collider.tag == "Dog" && aC.dog == true)
                {
                    Destroy(GameObject.FindWithTag("PuzzleDoor"));
                }
                if (hit.collider.name == "raftpart")
                {
                    Debug.Log("hit raftpart");
                    raftparts += 1;
                    Debug.Log("you have: " + raftparts + " raftparts!!");
                }
            }
            if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 2))
            {
                
                
                Debug.Log("Hit something");
                IInteractable i = hit.collider.gameObject.GetComponent<IInteractable>();
                
                if (i != null)
                {
                    Debug.Log("Hit an interactable.");
                    i.Action(this);
                }
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "escape")
        {
            if (raftparts > 4)
            {
                Debug.Log("you have enough parts and escaped!!!!!");
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("you dont have enough parts yet.....");
            }
        }
    }


    public bool AddItem(Item i)
    {
        return inventory.AddItem(i);
    }

    public bool CanOpenDoor(int id)
    {
        return inventory.CanOpenDoor(id);
    }
     
}
