using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameStart : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        GameManager a = GameManager.Instance;
    }
}
