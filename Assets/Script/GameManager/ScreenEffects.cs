using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform);
    }
}
