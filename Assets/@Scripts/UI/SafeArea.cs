using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea
{
    public const float SCREEN_WIDTH = 1080;
    public const float SCREEN_HEIGHT = 1920;
    public static void SetSafeArea(RectTransform rectTransform)
    {
        Rect safeArea = Screen.safeArea;
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = minAnchor + safeArea.size;
        Vector2 newMinPos = minAnchor;
        Vector2 newMaxPos = maxAnchor;

        if (safeArea.width * SCREEN_HEIGHT < SCREEN_WIDTH * safeArea.height)
        {
            float newHeight = safeArea.width * (SCREEN_HEIGHT / SCREEN_WIDTH);

            float minX = minAnchor.x;
            float minY = minAnchor.y + (safeArea.height - newHeight) / 2.0f;

            float maxX = maxAnchor.x;
            float maxY = maxAnchor.y - (safeArea.height - newHeight) / 2.0f;

            newMinPos = new Vector2(minX, minY);
            newMaxPos = new Vector2(maxX, maxY);
        }
        else
        {
            float newWidth = safeArea.height * (SCREEN_WIDTH / SCREEN_HEIGHT);

            float minY = minAnchor.y;
            float minX = minAnchor.x + (safeArea.width - newWidth) / 2.0f;

            float maxY = maxAnchor.y;
            float maxX = maxAnchor.x - (safeArea.width - newWidth) / 2.0f;

            newMinPos = new Vector2(minX, minY);
            newMaxPos = new Vector2(maxX, maxY);
        }

        newMinPos.x /= Mathf.Max(0.001f, (float)Screen.width);
        newMinPos.y /= Mathf.Max(0.001f, (float)Screen.height);
        newMaxPos.x /= Mathf.Max(0.001f, (float)Screen.width);
        newMaxPos.y /= Mathf.Max(0.001f, (float)Screen.height);

        rectTransform.anchorMin = newMinPos;
        rectTransform.anchorMax = newMaxPos;
    }


}