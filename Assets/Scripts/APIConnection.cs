using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class APIConnection : MonoBehaviour
{
    //public static APIConnection instance;
    public RawImage Fox;
    public float time = 0.0f;
    public float Switch = 5f;
    public int dogFox;
    public bool dog;

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                    JSONNode JsonObject = JSON.Parse(webRequest.downloadHandler.text);
                    if (dogFox == 1)
                    {
                        JSONNode imageName = JsonObject["image"];
                        Debug.Log("img name:" + imageName);
                        string imgUrl = (string)imageName;
                        StartCoroutine(GetTexture(imgUrl));
                        dog = false;
                    }
                    else
                    {
                        JSONNode imageName = JsonObject["url"];
                        Debug.Log("img name:" + imageName);
                        string imgUrl = (string)imageName;
                        dog = true;
                        StartCoroutine(GetTexture(imgUrl));
                        
                    }
                    
                    
                    
                    Debug.Log("check out this amazing joke: " + JsonObject["value"]["joke"].Value);
                    Debug.Log("The id of the joke is: " + JsonObject["value"]["id"].AsInt);

                    break;
            }
        }
    }
    void ReceivedTextureHandler(Texture2D texture2d)
    {
        Fox.texture = texture2d;
    }
    IEnumerator GetTexture(string imgUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imgUrl))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                //Texture2D myTexture2D = DownloadHandlerTexture.GetContent(uwr);
                ReceivedTextureHandler(DownloadHandlerTexture.GetContent(uwr));
            }
        }
    }


    void Start()
    {
       // instance = this;
        dog = false;
        dogFox = Random.Range(1, 3);
        if (dogFox == 1)
        {
            StartCoroutine(GetRequest("https://randomfox.ca/floof/"));
            Debug.Log(dogFox);
            Debug.Log(dog);
        }
        else
        {
            StartCoroutine(GetRequest("https://random.dog/woof.json"));
            Debug.Log(dogFox);
            dog = true;
            Debug.Log(dog);
        }
        
    }
    public void Update()
    {
        time += Time.deltaTime;
        if (time >= Switch)
        {
            time = 0f;
            Switch = 5f;
            if (dogFox == 1)
            {
                StartCoroutine(GetRequest("https://randomfox.ca/floof/"));
            }
            else
            {
                StartCoroutine(GetRequest("https://random.dog/woof.json"));
            }

        }
    }
}
