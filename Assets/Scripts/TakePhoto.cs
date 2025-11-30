using System;
using System.IO;
using System.Net;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public RenderTexture mySpecialTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
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
        var msgJson = "{'filepath': " + filePath + "}";
        if (Input.GetKeyDown(KeyCode.Z)) {
            SaveTextureToFileUtility.SaveRenderTextureToFile(mySpecialTexture, filePath);
            UnityEditor.AssetDatabase.Refresh();
            sendPOST(url, msgJson);
        }

    }
}
