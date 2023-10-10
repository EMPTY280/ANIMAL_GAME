using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{    
    [SerializeField] PlayerBase player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputP();
    }

    private void InputP()
    {
        //if (!IsInGame()) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.LeftButtonAction("Down");
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            player.LeftButtonAction("Stay");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            player.LeftButtonAction("Up");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.RightButtonAction("Down");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player.RightButtonAction("Stay");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            player.RightButtonAction("Up");
        }
    }
}