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

        if (!File.Exists(path))
        {
            UnityEngine.Debug.LogError("File not found at: " + path);
            yield break;
        }

        byte[] imageBytes = File.ReadAllBytes(path);

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageBytes, "image.jpg", "image/jpeg");

        print("upload");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                print(www.error);
            }
            else
            {
                print("Form upload complete!");
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
        var url = "http://192.168.43.62:5000/";
        var filePath = "Assets/Photos/image.jpg";
        if (Input.GetKeyDown(KeyCode.Z)) {
            SaveTextureToFileUtility.SaveRenderTextureToFile(mySpecialTexture, filePath);
            UnityEditor.AssetDatabase.Refresh();
            StartCoroutine(Upload(url, filePath));
        }

    }
}
