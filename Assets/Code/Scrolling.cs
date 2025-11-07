using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{
    [Header("Properties Parallax")]
    public List<Image> properties = new List<Image>();

    [Header("Scroll Settings")]
    public float scrollSpeed = 100f; 

    private float imageWidth;

    void Start()
    {
        if (properties.Count < 2)
        {
            Debug.LogError("Please assign at least 2 background images!");
            return;
        }
        imageWidth = properties[0].rectTransform.rect.width;
        properties[1].rectTransform.anchoredPosition = 
            new Vector2(imageWidth, properties[1].rectTransform.anchoredPosition.y);
    }

    void Update()
    {
        foreach (var img in properties)
        {
            var pos = img.rectTransform.anchoredPosition;
            pos.x -= scrollSpeed * Time.deltaTime;
            img.rectTransform.anchoredPosition = pos;
        }

        for (int i = 0; i < properties.Count; i++)
        {
            var img = properties[i];
            var pos = img.rectTransform.anchoredPosition;

            if (pos.x <= -imageWidth)
            {
                float rightMostX = GetRightMostImageX();

                pos.x = rightMostX + imageWidth; 
                img.rectTransform.anchoredPosition = pos;
            }
        }
    }

    private float GetRightMostImageX()
    {
        float maxX = float.MinValue;
        foreach (var img in properties)
        {
            float x = img.rectTransform.anchoredPosition.x;
            if (x > maxX)
                maxX = x;
        }
        return maxX;
    }
}
