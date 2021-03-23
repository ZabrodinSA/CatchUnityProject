using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class APIClient : MonoBehaviour
{
    public string getUrl;
    public string postUrl;
    public GameObject controller;
    public List<GameObject> Players;
    private GameController gameController;
    private Text[] namePlayers = new Text[10];
    private Text[] playersScore = new Text[10];
    void Start()
    {
        gameController = controller.GetComponent<GameController>();
        int count = 0;
        foreach (var item in Players)
        {
            Text[] texts = item.GetComponentsInChildren<Text>();
            namePlayers[count] = texts[0];
            playersScore[count] = texts[1];
            count++;
        }
    }

    public IEnumerator Get(string url) 
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    var player = JsonUtility.FromJson<Player>(result);

                    Debug.Log("Player name is " + player.name);
                    Debug.Log("His score " + player.score);
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    public IEnumerator Post(string url, Player player)
    {
        var jsonData = JsonUtility.ToJson(player);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"result\":" + result + "}";
                    var resultPlayerList = JSONHelper.FromJson<Player>(result);
                    int count = 0;
                    foreach (var item in resultPlayerList)
                    {
                        namePlayers[count].text = $"{count+1}. {item.name}";
                        playersScore[count].text = item.score.ToString();
                        if (count > 8) break;
                        count++;
                    }
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }

    }


    public void SendPlayerInServer()
    {
        var player = new Player()
        {
            id = 1,
            name = gameController.name,
            score = gameController.score
        };
        StartCoroutine(Post(postUrl, player));
    }

}
