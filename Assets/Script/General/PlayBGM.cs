using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    [SerializeField] private string BGMname = null;

    void Start()
    {
        if (BGMname == null)
            return;
        GameManager.Instance.SoundManager.PlayBGM(BGMname);
    }
}
