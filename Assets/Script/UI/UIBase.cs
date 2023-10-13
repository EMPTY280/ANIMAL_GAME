using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Button[] buttons;    

    protected virtual void Awake()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            string buttonName = buttons[i].name;
            buttons[i].onClick.AddListener(() => { OnClickButton(buttonName); });
        }
    }

    protected virtual void Start()
    {
        
    }

    void OnClickButton(string buttonName)
    {
        ButtonFunction(buttonName);
    }

    protected virtual void ButtonFunction(string buttonName)
    {

    }
}
