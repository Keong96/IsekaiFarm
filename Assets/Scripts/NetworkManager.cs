using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum EndPoint
{
    Test,
    Login,
    Users,
    GetFarm,
    LoadFarm,
    Plant
}

[System.Serializable]
public class TokenData
{
    public int userId;
    public string token;
}

public class NetworkManager : Singleton<NetworkManager>
{
    public string baseUrl = "";
    public TokenData tokenData;
    public Dictionary<EndPoint, string> endPoints = new Dictionary<EndPoint, string>
    {
        { EndPoint.Test, "" },
        { EndPoint.Login, "login" },
        { EndPoint.Users, "users" },
        { EndPoint.GetFarm, "getFarm" },
        { EndPoint.LoadFarm, "loadFarm" },
        { EndPoint.Plant, "plant" },

    };

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }

    public void GetAPIbaseUrl(string baseUrl)
    {
        this.baseUrl = baseUrl;
    }

    public void GetTokenFromURL(string token)
    {
        tokenData = new TokenData();
        tokenData = JsonUtility.FromJson<TokenData>(token);

        GameManager.Instance.ShowFarmList(tokenData.userId);
    }

    public void SetLoginInfo(string token)
    {
        string[] loginInfo = token.Split(",");

        tokenData = new TokenData();
        tokenData.userId = int.Parse(loginInfo[0]);
        tokenData.token = loginInfo[1];
    }

    public IEnumerator Get(EndPoint key, string parameter, Action<string> OnComplete)
    {
#if UNITY_EDITOR
        //baseUrl = "https://isekaifarmapi.onrender.com/";
        baseUrl = "localhost:8081/";
#endif
        if (baseUrl == string.Empty) yield break;

        string url = baseUrl + endPoints[key];

        if (!string.IsNullOrEmpty(parameter))
        {
            url += "?" + parameter;
        }

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
            request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
            request.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            if (tokenData != null && !string.IsNullOrEmpty(tokenData.token))
                request.SetRequestHeader("Authorization", "Bearer " + tokenData.token);

            //request.SetRequestHeader("Accept-Language", LocalizationSettings.SelectedLocale.Identifier.Code);

            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                ResponseBody responseBody = JsonUtility.FromJson<ResponseBody>(request.downloadHandler.text);
                Debug.Log("<color=red>" + key + "\n" + url + "</color>\n" + request.error);
                if (request.responseCode == 401) // If token session expired.
                {
                    tokenData = null;
                }
            }
            else
            {
                Debug.Log("<color=green>" + key + "\n" + url +"</color>\n" + request.downloadHandler.text);
                OnComplete.Invoke(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator Post(EndPoint key, string body, Action<string> OnComplete)
    {
#if UNITY_EDITOR
        baseUrl = "https://isekaifarmapi.onrender.com/";
#endif
        if (baseUrl == string.Empty) yield break;

        string url = baseUrl + endPoints[key];
        using (UnityWebRequest request = UnityWebRequest.Post(url, body))
        {
            request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
            request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
            request.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            if (tokenData != null && !string.IsNullOrEmpty(tokenData.token))
                request.SetRequestHeader("Authorization", "Bearer " + tokenData.token);

            //request.SetRequestHeader("Accept-Language", LocalizationSettings.SelectedLocale.Identifier.Code);

            byte[] jsonToSend = System.Text.Encoding.UTF8.GetBytes(body);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                ResponseBody responseBody = JsonUtility.FromJson<ResponseBody>(request.downloadHandler.text);
                Debug.Log("<color=red>" + key + "\n" + url + "</color>\n" + request.error + "/n" + request.downloadHandler.text);
                if (request.responseCode == 401) // If token session expired.
                {
                    tokenData = null;
                }
            }
            else
            {
                Debug.Log("<color=green>" + key + "\n" + url + "</color>\n" + request.downloadHandler.text);
                OnComplete.Invoke(request.downloadHandler.text);
            }
        }
    }
}

[Serializable]
public class ResponseBody
{
    public bool status;
    public string data;
    public string message;
}

[Serializable]
public class ResponseBody<T> where T : class
{
    public bool status;
    public T data;
    public string message;
}

[Serializable]
public class PaginatedData<T> where T : class
{
    public int page;
    public int pageSize;
    public int totalItems;
    public int totalPages;
    public T[] items;
}