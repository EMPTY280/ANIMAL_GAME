using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject magnet;
    [SerializeField] Re_Player player;
    [SerializeField] ObstacleAniPool aniPool;
    [SerializeField] Re_PlayerInput input;
    public ObstacleAniPool AniPool => aniPool;

    [SerializeField] List<RuntimeAnimatorController> characters = new List<RuntimeAnimatorController>();

    // Start is called before the first frame update
    void Start()
    {
        //SetPlayerCharacter();
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

    private void SetPlayerCharacter()
    {
        Animator playerCharacter = player._Animator;
        Characters characterNum = GameManager.Instance.Character;
        switch (characterNum)
        {
            case Characters.RABBIT:
                if (characters[0] != null)
                    playerCharacter.runtimeAnimatorController = characters[0];                
                break;

            case Characters.CAT:
                if (characters[1] != null)
                    playerCharacter.runtimeAnimatorController = characters[1];
                break;

            default:
                if (characters[0] != null)
                    playerCharacter.runtimeAnimatorController = characters[0];
                break;
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
