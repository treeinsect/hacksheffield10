using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public RenderTexture mySpecialTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            SaveTextureToFileUtility.SaveRenderTextureToFile(mySpecialTexture, "Assets/Photos/image");
            UnityEditor.AssetDatabase.Refresh();
        }

    }
}
