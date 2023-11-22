using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    SoundManager soundManager;
    private bool coinSoundQueued = false;

    [SerializeField] private Slider master, bgm, sfx;

    private void Awake()
    {
        soundManager = GameManager.Instance.SoundManager;
        master.value = soundManager.GetVolumeMaster();
        bgm.value = soundManager.GetVolumeBGM();
        sfx.value = soundManager.GetVolumeSFX();
    }

    private void Update()
    {
        if (coinSoundQueued && !Input.anyKey)
        {
            soundManager.PlaySFX("sliding");
            coinSoundQueued = false;
        }
    }

    public void SetMaster(float v)
    {
        soundManager.SetVolumeMaster(v);
    }
    public void SetBGM(float v)
    {
        soundManager.SetVolumeBGM(v);
    }
    public void SetSFX(float v)
    {
        soundManager.SetVolumeSFX(v);
        coinSoundQueued = true;
    }
}