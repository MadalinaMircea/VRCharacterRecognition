using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    private int textureSize = 600;
    private int penSize = 15;
    private Texture2D texture;
    private Color[] color;

    private bool isTouching;
    private bool touchingLast;

    private float posX, posY;
    private float lastX, lastY;

    float lastDrew;
    bool hasDrawn;

    Renderer renderer;

    public TextMesh text;

    CNNController controller;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        InitializeTexture();

        SetColor(Color.black);

        lastDrew = Time.time;

        controller = GetComponent<CNNController>();

        text.text = " ";
    }

    void InitializeTexture()
    {
        texture = new Texture2D(textureSize, textureSize);

        renderer.material.mainTexture = texture;

        texture.SetPixels(Enumerable.Repeat(Color.white, textureSize * textureSize).ToArray());

        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasDrawn)
        {
            if(Time.time - lastDrew >= 2)
            {
                controller.SendImage(texture);
                InitializeTexture();
                hasDrawn = false;
            }
        }

        int x = (int)(posX * textureSize + (penSize / 2));
        int y = (int)(posY * textureSize + (penSize / 2));

        if(touchingLast)
        {
            Debug.Log(color[0]);
            texture.SetPixels(x, y, penSize, penSize, color);

            //for (float t = 0.01f; t < 1.0f; t += 0.01f)
            //{
            //    int lerpX = (int)Mathf.Lerp(lastX, (float)x, t);
            //    int lerpY = (int)Mathf.Lerp(lastY, (float)y, t);
            //    texture.SetPixels(lerpX, lerpY, penSize, penSize, color);
            //}

            texture.Apply();

            lastDrew = Time.time;
            hasDrawn = true;

            text.text = " ";
        }

        lastX = x;
        lastY = y;

        touchingLast = isTouching;
    }

    public void SetTouch(bool touching)
    {
        isTouching = touching;
    }

    public void SetTouchPosition(float x, float y)
    {
        posX = x;
        posY = y;
    }

    public void SetColor(Color c)
    {
        color = Enumerable.Repeat(c, penSize * penSize).ToArray();
    }
}
