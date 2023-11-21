using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject magnet;
    [SerializeField] Re_Player player;
    [SerializeField] ObstacleAniPool aniPool;
    [SerializeField] Re_PlayerInput input;
    public ObstacleAniPool AniPool => aniPool;

    // Start is called before the first frame update
    void Start()
    {
        OffMagnet();
    }

    // Update is called once per frame
    void Update()
    {
        if (magnet.activeSelf == true)
        {
            magnet.transform.position = player.transform.position + Vector3.up;
        }
    }

    public void OnMagnet()
    {
        magnet.SetActive(true);
    }

    public void OffMagnet()
    {
        magnet.SetActive(false);
    }

    public void InputOff()
    {
        input.AbleInput = false;
    }
}
