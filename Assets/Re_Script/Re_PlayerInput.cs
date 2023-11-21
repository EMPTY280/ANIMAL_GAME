using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_PlayerInput : MonoBehaviour
{
    [SerializeField] Re_Player player;

    private bool leftButtonDowning;
    private bool rightButtonDowning;

    bool ableInput = true;
    public bool AbleInput { set { ableInput = value; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (leftButtonDowning)
            {
                player.LeftStay();
            }

            if (rightButtonDowning)
            {
                player.RightStay();
            }

        }

        InputP();
    }

    private void InputP()
    {
        //if (!IsInGame()) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.LeftDown();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            player.LeftStay();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            player.LeftUp();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.RightDown();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player.RightStay();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            player.RightUp();
        }
    }

    public void LeftButtonInput(string state)
    {
        if (!ableInput) return;

        switch (state)
        {
            case "Down":
                if (Time.timeScale > 0)
                {
                    player.LeftDown();
                }
                leftButtonDowning = true;
                break;

            case "Up":
                if (Time.timeScale > 0)
                {
                    player.LeftUp();
                }
                leftButtonDowning = false;
                break;
        }
    }

    public void RightButtonInput(string state)
    {
        if (!ableInput) return;

        switch (state)
        {
            case "Down":
                player.RightDown();
                rightButtonDowning = true;
                break;

            case "Up":
                player.RightUp();
                rightButtonDowning = false;
                break;
        }
    }
}