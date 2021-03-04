using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class APITest : MonoBehaviour
{
    string randomSong;

    public Text lyricsText;
    public InputField awnser;

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log("Received: " + webRequest.downloadHandler.text);
                    JSONNode JsonObject = JSON.Parse(webRequest.downloadHandler.text);

                    string lyrics = JsonObject["lyrics"];
                    lyrics = lyrics.Substring(lyrics.IndexOf('\n'));
                    Debug.Log(lyrics);

                    lyricsText.text = lyrics;

                    break;
            }
        }
    }

    void Start()
    {
        int random;

        random = Random.Range(0, 6);

        if (random == 0)
        {
            randomSong = "Aries";
        }

        if (random == 1)
        {
            randomSong = "19-2000";
        }

        if (random == 2)
        {
            randomSong = "Clint eastwood";
        }

        if (random == 3)
        {
            randomSong = "Dead butterflies";
        }

        if (random == 4)
        {
            randomSong = "Broken";
        }

        if (random == 5)
        {
            randomSong = "Dare";
        }

        StartCoroutine(GetRequest("https://api.lyrics.ovh/v1/Gorillaz/" + randomSong));
    }

    void Update()
    {
        if (awnser.text.ToLower() == randomSong.ToLower() && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Correct");

            lyricsText.enabled = false;
        }
    }
}
