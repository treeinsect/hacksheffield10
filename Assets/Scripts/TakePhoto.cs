using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Diagnostics;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public RenderTexture mySpecialTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    IEnumerator Upload(string url, string path)
    {
        WWWForm form = new WWWForm();
        form.AddField("filepath", path);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public void sendPOST(string url, string json)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var url = "192.168.0.1";
        var filePath = "Assets/Photos/image";
        if (Input.GetKeyDown(KeyCode.Z)) {
            SaveTextureToFileUtility.SaveRenderTextureToFile(mySpecialTexture, filePath);
            UnityEditor.AssetDatabase.Refresh();
            Upload(url, filePath);
        }

    }
}
