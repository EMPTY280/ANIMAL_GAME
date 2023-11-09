using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    RectTransform rect = null;
    TextMeshProUGUI textMeshPro = null;

    Vector2 initialSize = Vector2.zero;
    float currentSize = 1.0f;
    [SerializeField] float minSIze = 0.9f;
    float initialFontSize = 0f;

    bool isMouseDown = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        if (rect == null)
        {
            enabled = false;
            return;
        }
        initialSize = rect.sizeDelta;

        if (textMeshPro != null)
            initialFontSize = textMeshPro.fontSize;
    }

    private void OnDisable()
    {
        UpdateSize(1.0f);
    }

    private void Update()
    {
        if (isMouseDown)
            return;

        UpdateSize();

        currentSize = Mathf.Min(currentSize + Time.unscaledDeltaTime , 1.0f);
    }

    private void UpdateSize(float size = -1.0f)
    {
        if (size >= 0.0f)
            currentSize = size;

        rect.sizeDelta = initialSize * currentSize;
        if (textMeshPro != null)
        {
            textMeshPro.fontSize = initialFontSize * currentSize;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        UpdateSize(minSIze);
        isMouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        isMouseDown = false;
    }
}
