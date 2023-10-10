using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
    private Image blackout;

    private void Awake()
    {
        blackout = GetComponent<Image>();
    }

    public void SetBlackout(bool active)
    {
        Color newColor = blackout.color;
        newColor.a = 0.65f * (active ? 1.0f : 0.0f);
        blackout.color = newColor;
    }
}
