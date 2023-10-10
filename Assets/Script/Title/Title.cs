using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{

    void Update()
    {
        if (Input.anyKey)
        {
            GameManager.GetInstance().SetBlackout(true);
        }
    }
}
