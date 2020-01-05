using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CNNController : MonoBehaviour
{
    bool hasSent = false;

    string path = "C:\\Users\\Madalina\\Desktop\\UnityCNN\\image";

    public TextMesh text;
    void Start()
    {
    }
    void Update()
    {
        if(hasSent)
        {
            CheckForReply();
        }
    }

    public void SendImage(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToJPG();

        File.WriteAllBytes(path + ".jpg", bytes);

        hasSent = true;
    }

    void CheckForReply()
    {
        string filePath = path + ".txt";
        if (File.Exists(filePath))
        {
            hasSent = false;
            text.text = File.ReadAllText(filePath);
            File.Delete(filePath);
        }
    }
}
