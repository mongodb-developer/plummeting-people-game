using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class PlayerData
{

    public string plummie_tag;
    public int collisions;
    public int steps;

    public string Stringify() {
        return JsonUtility.ToJson(this);
    }

    public static PlayerData Parse(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }

    public IEnumerator FetchPlayerData(System.Action<PlayerData> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/plummies/" + this.plummie_tag))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(null);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(PlayerData.Parse(request.downloadHandler.text));
                }
            }
        }
    }

    public IEnumerator SavePlayerData(System.Action<bool> callback = null)
    {
        using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/plummies", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(this.Stringify());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if(callback != null) {
                    callback.Invoke(false);
                }
            }
            else
            {
                // Debug.Log(request.downloadHandler.text);
                if(callback != null) {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }

}
