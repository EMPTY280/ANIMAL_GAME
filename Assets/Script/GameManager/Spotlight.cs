using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spotlight : MonoBehaviour
{
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetRect(Rect rect)
    {
        rectTransform.anchoredPosition = new Vector2(rect.x, rect.y);
        rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
    }
}
