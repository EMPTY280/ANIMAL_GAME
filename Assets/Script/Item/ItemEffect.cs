using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void EndEffect()
    {
        gameObject.SetActive(false);
    }
}
