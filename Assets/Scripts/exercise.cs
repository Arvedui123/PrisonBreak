using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exercise : MonoBehaviour
{

    private List<string> words;
    // Start is called before the first frame update
    void Start()
    {
        words = new List<string>();

        Debug.Log(words.Capacity);

        words.Add(item:"david");
        words.Add(item:"is");
        words.Add(item: "super");
        words.Add(item: "cool");

        Debug.Log(words[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
