using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{    
    [SerializeField] PlayerBase player;

    private bool leftButtonDowning;
    private bool rightButtonDowning;

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
                player.LeftButtonAction("Stay");
            }

            if (rightButtonDowning)
            {
                player.RightButtonAction("Stay");
            }

        }

        //InputP();
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

    public void LeftButtonInput(string state)
    {
        switch (state)
        {
            case "Down":
                if (Time.timeScale > 0)
                {
                    player.LeftButtonAction(state);
                }
                leftButtonDowning = true;
                break;

            case "Up":
                if (Time.timeScale > 0)
                {
                    player.LeftButtonAction(state);
                }
                leftButtonDowning = false;
                break;
        }
    }

    public void RightButtonInput(string state)
    {
        switch (state)
        {
            case "Down":
                player.RightButtonAction(state);
                rightButtonDowning = true;
                break;

            case "Up":
                player.RightButtonAction(state);
                rightButtonDowning = false;
                break;
        }
    }
}